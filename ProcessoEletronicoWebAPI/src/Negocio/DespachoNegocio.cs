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
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;

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

        private DespachoValidacao _validacao;
        private AnexoValidacao _anexoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private OrganogramaValidacao _organogramaValidacao;

        public DespachoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, IProcessoNegocio processoNegocio, ICurrentUserProvider user, OrganogramaValidacao organogramaValidacao)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _processoNegocio = processoNegocio;
            _repositorioDespachos = repositorios.Despachos;
            _repositorioProcessos = repositorios.Processos;
            _repositorioAnexos = repositorios.Anexos;
            _validacao = new DespachoValidacao(repositorios);
            _anexoValidacao = new AnexoValidacao(repositorios);
            _usuarioValidacao = new UsuarioValidacao();
            _organogramaValidacao = organogramaValidacao;
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

        public DespachoModeloNegocio Pesquisar(int idDespacho)
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
            InformacoesOrganizacao(despacho);
            InformacoesUnidade(despacho);
            InformacoesUsuario(despacho);

            _repositorioDespachos.Add(despacho);
            _unitOfWork.Save();

            return Pesquisar(despacho.Id);
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
            OrganizacaoOrganogramaModelo organizacao = _organogramaValidacao.PesquisarOrganizacao(despacho.GuidOrganizacaoDestino);

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização autuadora não encontrada no Organograma");
            }

            despacho.GuidOrganizacaoDestino = new Guid(organizacao.guid);
            despacho.NomeOrganizacaoDestino = organizacao.razaoSocial;
            despacho.SiglaOrganizacaoDestino = organizacao.sigla;

        }
        private void InformacoesUnidade(Despacho despacho)
        {
            UnidadeOrganogramaModelo unidade = _organogramaValidacao.PesquisarUnidade(despacho.GuidUnidadeDestino);

            if (unidade == null)
            {
                throw new RequisicaoInvalidaException("Unidade autudora não encontrada no Organograma");
            }

            despacho.GuidUnidadeDestino = new Guid(unidade.guid);
            despacho.NomeUnidadeDestino = unidade.nome;
            despacho.SiglaUnidadeDestino = unidade.sigla;
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
