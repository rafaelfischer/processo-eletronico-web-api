using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Negocio.Notificacoes.Base;
using Negocio.Bloqueios;
using Negocio.Bloqueios.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Negocio.RascunhosDespacho.Validations.Base;

namespace ProcessoEletronicoService.Negocio
{
    public class DespachoNegocio : IDespachoNegocio 
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IProcessoNegocio _processoNegocio;
        private IRepositorioGenerico<Anexo> _repositorioAnexos;
        private IRepositorioGenerico<Despacho> _repositorioDespachos;
        private IRepositorioGenerico<Processo> _repositorioProcessos;
        private IRepositorioGenerico<RascunhoDespacho> _repositorioRascunhosDespacho;
        private INotificacoesService _notificacoesService;
        private IBloqueioCore _bloqueioCore;
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;

        private DespachoValidacao _validacao;
        private AnexoValidacao _anexoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private BloqueioValidation _bloqueioValidation;
        private IRascunhoDespachoValidation _rascunhoDespachoValidation;

        public DespachoNegocio(IProcessoEletronicoRepositorios repositorios, 
            IMapper mapper, IProcessoNegocio processoNegocio, INotificacoesService notificacoesService, 
            ICurrentUserProvider user, IOrganizacaoService organizacaoService, IUnidadeService unidadeService, 
            BloqueioValidation bloqueioValidation, IBloqueioCore bloqueioCore, IRascunhoDespachoValidation rascunhoDespachoValidation)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _processoNegocio = processoNegocio;
            _repositorioDespachos = repositorios.Despachos;
            _repositorioProcessos = repositorios.Processos;
            _repositorioAnexos = repositorios.Anexos;
            _repositorioRascunhosDespacho = repositorios.RascunhosDespacho;
            _notificacoesService = notificacoesService;
            _validacao = new DespachoValidacao(repositorios);
            _anexoValidacao = new AnexoValidacao(repositorios);
            _usuarioValidacao = new UsuarioValidacao();
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;
            _bloqueioCore = bloqueioCore;
            _bloqueioValidation = bloqueioValidation;
            _rascunhoDespachoValidation = rascunhoDespachoValidation;
        }
        
        public List<DespachoModeloNegocio> PesquisarDespachosUsuario()
        {
            IQueryable<Despacho> query;
            query = _repositorioDespachos;

            query = query.Where(d => d.IdUsuarioDespachante.Equals(_user.UserCpf))
                         .Include(p => p.Processo)
                         .Include(a => a.Anexos).ThenInclude(a => a.TipoDocumental);

            List<Despacho> despachos = query.ToList();
            LimparConteudoAnexos(despachos);
            
            return _mapper.Map<List<DespachoModeloNegocio>>(query.ToList());
        }

        public DespachoModeloNegocio PesquisarComProcesso(int idDespacho)
        {
            Despacho despacho = _repositorioDespachos.Where(d => d.Id == idDespacho)
                                                    .Include(p => p.Processo)
                                                    .Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental)
                                                    .SingleOrDefault();

            _validacao.Existe(despacho);

            //Limpando conteúdo dos anexos para não ser enviado dentro do despacho
            if (despacho.Anexos != null)
            {
                LimparConteudoAnexos(despacho.Anexos);
            }


            return _mapper.Map<DespachoModeloNegocio>(despacho);
        }

        public DespachoModeloNegocio Pesquisar(int idProcesso, int idDespacho)
        {
            Despacho despacho = _repositorioDespachos.Where(d => d.Id == idDespacho && d.IdProcesso == idProcesso)
                                                    .Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental)
                                                    .SingleOrDefault();

            _validacao.Existe(despacho);
            return _mapper.Map<DespachoModeloNegocio>(despacho);
        }

        public DespachoModeloNegocio Despachar(DespachoModeloNegocio despachoNegocio)
        {
            _validacao.Preenchido(despachoNegocio);
            
            //Obter id da atividade do processo para validação dos anexos do despacho
            int idAtividadeProcesso;
            try
            {
                idAtividadeProcesso = _repositorioProcessos.Where(p => p.Id == despachoNegocio.IdProcesso).Select(s => s.IdAtividade).SingleOrDefault();
            }
            catch (Exception)
            {
                idAtividadeProcesso = 0;
            }

            _validacao.Valido(idAtividadeProcesso, despachoNegocio, _user.UserGuidOrganizacaoPatriarca);

            /*Verificar se o usuário tem permissão para realizar o despacho na organização em que ele se encontra*/
            PermissaoDespacho(despachoNegocio);

            Despacho despacho = new Despacho();
            
            _mapper.Map(despachoNegocio, despacho);
            PreparaInsercaoDespacho(despacho);

            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _bloqueioValidation.IsProcessoBlockedByAnotherUsuarioOrSistema(despachoNegocio.IdProcesso);
            InformacoesOrganizacao(despacho);
            InformacoesUnidade(despacho);
            InformacoesUsuario(despacho);

            _repositorioDespachos.Add(despacho);
            _bloqueioCore.DeleteBloqueioOfProcessoIfExists(despachoNegocio.IdProcesso);
            _notificacoesService.Insert(despacho);
            _unitOfWork.Save();

            return PesquisarComProcesso(despacho.Id);
        }

        public DespachoModeloNegocio DespacharPorRascunho(int idProcesso, int idRascunhoDespacho)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(d => d.Id == idRascunhoDespacho).SingleOrDefault();
            _rascunhoDespachoValidation.Exists(rascunhoDespacho);
            

            DespachoModeloNegocio despachoModeloNegocio = _mapper.Map<DespachoModeloNegocio>(rascunhoDespacho);
            despachoModeloNegocio.IdProcesso = idProcesso;
            return Despachar(despachoModeloNegocio);
        }

        private void PermissaoDespacho(DespachoModeloNegocio despacho)
        {
            List<int> listaIdsProcessosNaOrganizacao;
            listaIdsProcessosNaOrganizacao = _processoNegocio.PesquisarProcessosNaOrganizacao(_user.UserGuidOrganizacao.ToString("D")).Select(p => p.Id).ToList();

            if (!listaIdsProcessosNaOrganizacao.Contains(despacho.IdProcesso))
            {
                throw new RequisicaoInvalidaException("O processo não se encontra na organização do usuário. Não é possível realizar o despacho.");
            }
        }

        private void PreparaInsercaoDespacho(Despacho despacho)
        {
            //Preenche processo dos anexos
            if (despacho.Anexos != null)
            {
                foreach (Anexo anexo in despacho.Anexos)
                {
                    anexo.IdProcesso = despacho.IdProcesso;
                }
            }

            //Data/hora atual do despacho
            despacho.DataHoraDespacho = DateTime.Now;
        }

        private void InformacoesOrganizacao(Despacho despacho)
        {
            Organizacao organizacao =  _organizacaoService.Search(despacho.GuidOrganizacaoDestino).ResponseObject;

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização autuadora não encontrada no Organograma");
            }

            despacho.GuidOrganizacaoDestino = new Guid(organizacao.Guid);
            despacho.NomeOrganizacaoDestino = organizacao.RazaoSocial;
            despacho.SiglaOrganizacaoDestino = organizacao.Sigla;

        }
        private void InformacoesUnidade(Despacho despacho)
        {
            Unidade unidade = _unidadeService.Search(despacho.GuidUnidadeDestino).ResponseObject;

            if (unidade == null)
            {
                throw new RequisicaoInvalidaException("Unidade autudora não encontrada no Organograma");
            }

            despacho.GuidUnidadeDestino = new Guid(unidade.Guid);
            despacho.NomeUnidadeDestino = unidade.Nome;
            despacho.SiglaUnidadeDestino = unidade.Sigla;
        }

        private void InformacoesUsuario(Despacho despacho)
        {
            despacho.IdUsuarioDespachante = _user.UserCpf;
            despacho.NomeUsuarioDespachante = _user.UserNome;
        }

        private void LimparConteudoAnexos(ICollection<Despacho> despachos)
        {
            if (despachos != null && despachos.Count() > 0)
            {
                foreach (Despacho despacho in despachos)
                {
                    LimparConteudoAnexos(despacho.Anexos);
                }
            }
        }

        private void LimparConteudoAnexos(ICollection<Anexo> anexos)
        {
            if (anexos != null)
            {
                foreach (Anexo anexo in anexos)
                {
                    anexo.Conteudo = null;
                }
            }
        }

        
    }
}
