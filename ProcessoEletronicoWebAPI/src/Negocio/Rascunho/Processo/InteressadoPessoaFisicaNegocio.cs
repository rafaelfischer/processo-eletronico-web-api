using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
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

namespace ProcessoEletronicoService.Negocio
{
    public class InteressadoPessoaFisicaNegocio : BaseNegocio, IInteressadoPessoaFisicaNegocio
    {
        private IRepositorioGenerico<InteressadoPessoaFisica> _repositorioInteressadosPessoaFisica;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private InteressadoPessoaFisicaValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;

        private IContatoNegocio contatoNegocio;
        private IEmailNegocio emailNegocio;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;


        public InteressadoPessoaFisicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, InteressadoPessoaFisicaValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, UsuarioValidacao usuarioValidacao)
        {
            _repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _usuarioValidacao = usuarioValidacao;
            contatoNegocio = new ContatoNegocio(repositorios);
            emailNegocio = new EmailNegocio(repositorios);
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;

        }

        public IList<InteressadoPessoaFisicaModeloNegocio> Get(int idRascunhoProcesso)
        {
            IList<InteressadoPessoaFisica> interessadoPessoaFisica = _repositorioInteressadosPessoaFisica.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso).Include(ipf => ipf.Contatos).Include(ipf => ipf.Emails).ToList();
            return _mapper.Map<IList<InteressadoPessoaFisicaModeloNegocio>>(interessadoPessoaFisica);
        }

        public InteressadoPessoaFisicaModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaFisica interessadoPessoaFisica = _repositorioInteressadosPessoaFisica.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.Contatos).Include(ipf => ipf.Emails).SingleOrDefault();
            _validacao.NaoEncontrado(interessadoPessoaFisica);
            return _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessadoPessoaFisica);
        }

        public InteressadoPessoaFisicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == UsuarioGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.NaoEncontrado(rascunhoProcesso);
            _validacao.Preenchido(interessadoPessoaFisicaNegocio);
            _validacao.Valido(interessadoPessoaFisicaNegocio);

            InteressadoPessoaFisica interessadoPessoaFisica = _mapper.Map<InteressadoPessoaFisica>(interessadoPessoaFisicaNegocio);
            InformacoesMunicipio(interessadoPessoaFisica);
            interessadoPessoaFisica.IdRascunhoProcesso = idRascunhoProcesso;

            _repositorioInteressadosPessoaFisica.Add(interessadoPessoaFisica);
            _unitOfWork.Save();

            return Get(idRascunhoProcesso, interessadoPessoaFisica.Id);

        }

        public void Patch(int idRascunhoProcesso, int id, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == UsuarioGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.NaoEncontrado(rascunhoProcesso);

            InteressadoPessoaFisica interessadoPessoaFisica = _repositorioInteressadosPessoaFisica.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).SingleOrDefault();
            _validacao.NaoEncontrado(interessadoPessoaFisica);
            _validacao.Preenchido(interessadoPessoaFisicaNegocio);
            _validacao.Valido(interessadoPessoaFisicaNegocio);

            MapInteressado(interessadoPessoaFisicaNegocio, interessadoPessoaFisica);
            InformacoesMunicipio(interessadoPessoaFisica);

            _unitOfWork.Save();

        }

        public void Delete(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaFisica interessadoPessoaFisica = _repositorioInteressadosPessoaFisica.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.Contatos).Include(ipf => ipf.Emails).SingleOrDefault();
            _validacao.NaoEncontrado(interessadoPessoaFisica);

            //Excluir emails e contatos (caso haja)
            emailNegocio.Delete(interessadoPessoaFisica.Emails);
            contatoNegocio.Delete(interessadoPessoaFisica.Contatos);
            _repositorioInteressadosPessoaFisica.Remove(interessadoPessoaFisica);
            _unitOfWork.Save();
        }

        public void Delete(ICollection<InteressadoPessoaFisica> interessadosPessoaFisica)
        {
            if (interessadosPessoaFisica != null)
            {
                foreach (var interessadoPessoaFisica in interessadosPessoaFisica)
                {
                    Delete(interessadoPessoaFisica);
                }
            }
        }

        public void Delete(InteressadoPessoaFisica interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica != null)
            {
                contatoNegocio.Delete(interessadoPessoaFisica.Contatos);
                emailNegocio.Delete(interessadoPessoaFisica.Emails);
                _repositorioInteressadosPessoaFisica.Remove(interessadoPessoaFisica);
            }
        }

        private void MapInteressado(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio, InteressadoPessoaFisica interessadoPessoaFisica)
        {
            interessadoPessoaFisica.Cpf = interessadoPessoaFisicaNegocio.Cpf;
            interessadoPessoaFisica.GuidMunicipio = new Guid(interessadoPessoaFisicaNegocio.GuidMunicipio);
            interessadoPessoaFisica.Nome = interessadoPessoaFisicaNegocio.Nome;
        }

        private void InformacoesMunicipio(InteressadoPessoaFisica interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica.GuidMunicipio != null)
            {
                MunicipioOrganogramaModelo municipioOrganograma = PesquisarMunicipio(interessadoPessoaFisica.GuidMunicipio);

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
