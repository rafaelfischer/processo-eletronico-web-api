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
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class InteressadoPessoaJuridicaNegocio : IInteressadoPessoaJuridicaNegocio
    {
        private IRepositorioGenerico<InteressadoPessoaJuridicaRascunho> _repositorioInteressadosPessoaJuridicaRascunho;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private InteressadoPessoaJuridicaValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private OrganogramaValidacao _organogramaValidacao;
        private IContatoInteressadoPessoaJuridicaNegocio _contatoNegocio;
        //private IEmailInteressadopessoaJuridicaNegocio _emailNegocio;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;


        public InteressadoPessoaJuridicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user, IContatoInteressadoPessoaJuridicaNegocio contatoNegocio, InteressadoPessoaJuridicaValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, UsuarioValidacao usuarioValidacao, OrganogramaValidacao organogramaValidacao)
        {
            _repositorioInteressadosPessoaJuridicaRascunho = repositorios.InteressadosPessoaJuridicaRascunho;
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

        public IList<InteressadoPessoaJuridicaModeloNegocio> Get(int idRascunhoProcesso)
        {
            IList<InteressadoPessoaJuridicaRascunho> interessadoPessoaJuridica = _repositorioInteressadosPessoaJuridicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).ToList();
            return _mapper.Map<IList<InteressadoPessoaJuridicaModeloNegocio>>(interessadoPessoaJuridica);
        }

        public InteressadoPessoaJuridicaModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica = _repositorioInteressadosPessoaJuridicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).SingleOrDefault();
            _validacao.Exists(interessadoPessoaJuridica);
            return _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(interessadoPessoaJuridica);
        }

        public InteressadoPessoaJuridicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == _user.UserGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);
            _validacao.IsFilled(interessadoPessoaJuridicaNegocio);
            _validacao.IsValid(interessadoPessoaJuridicaNegocio);

            InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica = _mapper.Map<InteressadoPessoaJuridicaRascunho>(interessadoPessoaJuridicaNegocio);
            InformacoesMunicipio(interessadoPessoaJuridica);
            interessadoPessoaJuridica.IdRascunhoProcesso = idRascunhoProcesso;

            _repositorioInteressadosPessoaJuridicaRascunho.Add(interessadoPessoaJuridica);
            _unitOfWork.Save();

            return Get(idRascunhoProcesso, interessadoPessoaJuridica.Id);
        }

        public void Patch(int idRascunhoProcesso, int id, InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(r => r.Id == idRascunhoProcesso && r.GuidOrganizacao == _user.UserGuidOrganizacao).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);

            InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica = _repositorioInteressadosPessoaJuridicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).SingleOrDefault();
            _validacao.Exists(interessadoPessoaJuridica);
            _validacao.IsFilled(interessadoPessoaJuridicaNegocio);
            _validacao.IsValid(interessadoPessoaJuridicaNegocio);

            MapInteressado(interessadoPessoaJuridicaNegocio, interessadoPessoaJuridica);
            InformacoesMunicipio(interessadoPessoaJuridica);

            _unitOfWork.Save();

        }

        public void Delete(int idRascunhoProcesso, int id)
        {
            InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica = _repositorioInteressadosPessoaJuridicaRascunho.Where(ipf => ipf.IdRascunhoProcesso == idRascunhoProcesso && ipf.Id == id).Include(ipf => ipf.ContatosRascunho).Include(ipf => ipf.EmailsRascunho).SingleOrDefault();
            _validacao.Exists(interessadoPessoaJuridica);

            //Excluir emails e contatos (caso haja)
            //_emailNegocio.Delete(interessadoPessoaFisica.EmailsRascunho);
            _contatoNegocio.Delete(interessadoPessoaJuridica.ContatosRascunho);
            _repositorioInteressadosPessoaJuridicaRascunho.Remove(interessadoPessoaJuridica);
            _unitOfWork.Save();
        }

        public void Delete(ICollection<InteressadoPessoaJuridicaRascunho> interessadosPessoaJuridica)
        {
            if (interessadosPessoaJuridica != null)
            {
                foreach (var interessadoPessoaJuridica in interessadosPessoaJuridica)
                {
                    Delete(interessadoPessoaJuridica);
                }
            }
        }

        public void Delete(InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica)
        {
            if (interessadoPessoaJuridica != null)
            {
                _contatoNegocio.Delete(interessadoPessoaJuridica.ContatosRascunho);
                //_emailNegocio.Delete(interessadoPessoaFisica.EmailsRascunho);
                _repositorioInteressadosPessoaJuridicaRascunho.Remove(interessadoPessoaJuridica);
            }
        }

        private void MapInteressado(InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio, InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica)
        {
            interessadoPessoaJuridica.Cnpj = interessadoPessoaJuridicaNegocio.Cnpj;
            interessadoPessoaJuridica.RazaoSocial = interessadoPessoaJuridicaNegocio.RazaoSocial;
            interessadoPessoaJuridica.Sigla = interessadoPessoaJuridicaNegocio.Sigla;
            interessadoPessoaJuridica.SiglaUnidade = interessadoPessoaJuridicaNegocio.SiglaUnidade;
            interessadoPessoaJuridica.NomeUnidade = interessadoPessoaJuridicaNegocio.NomeUnidade;
            interessadoPessoaJuridica.GuidMunicipio = new Guid(interessadoPessoaJuridicaNegocio.GuidMunicipio);
        }

        private void InformacoesMunicipio(InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica)
        {
            if (interessadoPessoaJuridica.GuidMunicipio.HasValue)
            {
                MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessadoPessoaJuridica.GuidMunicipio.Value);

                if (municipioOrganograma == null)
                {
                    throw new RequisicaoInvalidaException($"Municipio do interessado pessoa jurídica {interessadoPessoaJuridica.RazaoSocial} não encontrado no Organograma.");
                }

                interessadoPessoaJuridica.NomeMunicipio = municipioOrganograma.nome;
                interessadoPessoaJuridica.UfMunicipio = municipioOrganograma.uf;
            }
        }

    }
}
