﻿using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;
using System.Linq;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class InteressadoPessoaFisicaNegocio : IInteressadoPessoaFisicaNegocio
    {
        private IRepositorioGenerico<InteressadoPessoaFisicaRascunho> _repositorioInteressadosPessoaFisicaRascunho;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private InteressadoPessoaFisicaValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private OrganogramaValidacao _organogramaValidacao;

        private IContatoInteressadoPessoaFisicaNegocio _contatoNegocio;
        //private IEmailNegocio _emailNegocio;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;


        public InteressadoPessoaFisicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user, InteressadoPessoaFisicaValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, UsuarioValidacao usuarioValidacao, OrganogramaValidacao organogramaValidacao, IContatoInteressadoPessoaFisicaNegocio contatoNegocio)
        {
            _repositorioInteressadosPessoaFisicaRascunho = repositorios.InteressadosPessoaFisicaRascunho;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _usuarioValidacao = usuarioValidacao;
            _organogramaValidacao = organogramaValidacao;
            _contatoNegocio = contatoNegocio;
            //_emailNegocio = emailNegocio;
            _mapper = mapper;
            _user = user;
            _unitOfWork = repositorios.UnitOfWork;
        }

        public IList<InteressadoPessoaFisicaModeloNegocio> Get(int idRascunhoProcesso)
        {
            IList<InteressadoPessoaFisicaRascunho> interessadoPessoaFisica = _repositorioInteressadosPessoaFisicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).ToList();
            return _mapper.Map<IList<InteressadoPessoaFisicaModeloNegocio>>(interessadoPessoaFisica);
        }

        public InteressadoPessoaFisicaModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaFisicaRascunho interessadoPessoaFisica = _repositorioInteressadosPessoaFisicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).SingleOrDefault();
            _validacao.Exists(interessadoPessoaFisica);
            return _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessadoPessoaFisica);
        }

        public InteressadoPessoaFisicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == _user.UserGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);
            _validacao.IsFilled(interessadoPessoaFisicaNegocio);
            _validacao.IsValid(interessadoPessoaFisicaNegocio);

            InteressadoPessoaFisicaRascunho interessadoPessoaFisica = _mapper.Map<InteressadoPessoaFisicaRascunho>(interessadoPessoaFisicaNegocio);
            InformacoesMunicipio(interessadoPessoaFisica);
            interessadoPessoaFisica.IdRascunhoProcesso = idRascunhoProcesso;

            _repositorioInteressadosPessoaFisicaRascunho.Add(interessadoPessoaFisica);
            _unitOfWork.Save();

            return Get(idRascunhoProcesso, interessadoPessoaFisica.Id);

        }

        public void Patch(int idRascunhoProcesso, int id, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == _user.UserGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);

            InteressadoPessoaFisicaRascunho interessadoPessoaFisica = _repositorioInteressadosPessoaFisicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).SingleOrDefault();
            _validacao.Exists(interessadoPessoaFisica);
            _validacao.IsFilled(interessadoPessoaFisicaNegocio);
            _validacao.IsValid(interessadoPessoaFisicaNegocio);

            MapInteressado(interessadoPessoaFisicaNegocio, interessadoPessoaFisica);
            InformacoesMunicipio(interessadoPessoaFisica);

            _unitOfWork.Save();

        }

        public void Delete(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaFisicaRascunho interessadoPessoaFisica = _repositorioInteressadosPessoaFisicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).SingleOrDefault();
            _validacao.Exists(interessadoPessoaFisica);

            //Excluir emails e contatos (caso haja)
            //_emailNegocio.Delete(interessadoPessoaFisica.EmailsRascunho);
            _contatoNegocio.Delete(interessadoPessoaFisica.ContatosRascunho);
            _repositorioInteressadosPessoaFisicaRascunho.Remove(interessadoPessoaFisica);
            _unitOfWork.Save();
        }

        public void Delete(ICollection<InteressadoPessoaFisicaRascunho> interessadosPessoaFisica)
        {
            if (interessadosPessoaFisica != null)
            {
                foreach (var interessadoPessoaFisica in interessadosPessoaFisica)
                {
                    Delete(interessadoPessoaFisica);
                }
            }
        }

        public void Delete(InteressadoPessoaFisicaRascunho interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica != null)
            {
                _contatoNegocio.Delete(interessadoPessoaFisica.ContatosRascunho);
                //_emailNegocio.Delete(interessadoPessoaFisica.EmailsRascunho);
                _repositorioInteressadosPessoaFisicaRascunho.Remove(interessadoPessoaFisica);
            }
        }

        private void MapInteressado(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio, InteressadoPessoaFisicaRascunho interessadoPessoaFisica)
        {
            interessadoPessoaFisica.Cpf = interessadoPessoaFisicaNegocio.Cpf;
            interessadoPessoaFisica.GuidMunicipio = new Guid(interessadoPessoaFisicaNegocio.GuidMunicipio);
            interessadoPessoaFisica.Nome = interessadoPessoaFisicaNegocio.Nome;
        }

        private void InformacoesMunicipio(InteressadoPessoaFisicaRascunho interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica.GuidMunicipio.HasValue)
            {
                MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessadoPessoaFisica.GuidMunicipio.Value);

                if (municipioOrganograma == null)
                {
                    throw new RequisicaoInvalidaException("Municipio do interessado pessoa jurídica não encontrado no Organograma.");
                }

                interessadoPessoaFisica.NomeMunicipio = municipioOrganograma.nome;
                interessadoPessoaFisica.UfMunicipio = municipioOrganograma.uf;
            }
        }

    }
}
