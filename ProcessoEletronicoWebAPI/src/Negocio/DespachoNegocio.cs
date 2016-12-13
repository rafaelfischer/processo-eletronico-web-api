using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace ProcessoEletronicoService.Negocio
{
    public class DespachoNegocio : BaseNegocio, IDespachoNegocio
    {
        IUnitOfWork unitOfWork;
        IProcessoNegocio processoNegocio;
        IRepositorioGenerico<Anexo> repositorioAnexos;
        IRepositorioGenerico<Despacho> repositorioDespachos;
        IRepositorioGenerico<Processo> repositorioProcessos;

        DespachoValidacao despachoValidacao;
        AnexoValidacao anexoValidacao;
        UsuarioValidacao usuarioValidacao;

        public DespachoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            processoNegocio = new ProcessoNegocio(repositorios);
            repositorioDespachos = repositorios.Despachos;
            repositorioProcessos = repositorios.Processos;
            repositorioAnexos = repositorios.Anexos;
            despachoValidacao = new DespachoValidacao(repositorios);
            anexoValidacao = new AnexoValidacao(repositorios);
            usuarioValidacao = new UsuarioValidacao();
        }
        
        public List<DespachoModeloNegocio> PesquisarDespachosUsuario()
        {
            IQueryable<Despacho> query;
            query = repositorioDespachos;

            query = query.Where(d => d.IdUsuarioDespachante.Equals(UsuarioCpf))
                         .Include(p => p.Processo)
                         .Include(a => a.Anexos).ThenInclude(a => a.TipoDocumental);

            return Mapper.Map<List<Despacho>, List<DespachoModeloNegocio>>(query.ToList());
        }

        public DespachoModeloNegocio Pesquisar(int idDespacho)
        {
            Despacho despacho = repositorioDespachos.Where(d => d.Id == idDespacho)
                                                    .Include(p => p.Processo)
                                                    .Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental)
                                                    .SingleOrDefault();

            despachoValidacao.Existe(despacho);

            //Limpando conteúdo dos anexos para não ser enviado dentro do despacho
            if (despacho.Anexos != null)
            {
                LimparConteudoAnexos(despacho.Anexos);
            }


            return Mapper.Map<Despacho, DespachoModeloNegocio>(despacho);
        }

        public DespachoModeloNegocio Despachar(DespachoModeloNegocio despachoNegocio)
        {
            despachoValidacao.Preenchido(despachoNegocio);
            
            //Obter id da atividade do processo para validação dos anexos do despacho
            int idAtividadeProcesso;
            try
            {
                idAtividadeProcesso = repositorioProcessos.Where(p => p.Id == despachoNegocio.IdProcesso).Select(s => s.IdAtividade).SingleOrDefault();
            }
            catch (Exception)
            {
                idAtividadeProcesso = 0;
            }

            despachoValidacao.Valido(idAtividadeProcesso, despachoNegocio, UsuarioGuidOrganizacaoPatriarca);

            /*Verificar se o usuário tem permissão para realizar o despacho na organização em que ele se encontra*/
            PermissaoDespacho(despachoNegocio);

            Despacho despacho = new Despacho();
            PreparaInsercaoDespacho(despachoNegocio);
            Mapper.Map(despachoNegocio, despacho);

            usuarioValidacao.Autenticado(UsuarioCpf, UsuarioNome);
            usuarioValidacao.PossuiOrganizaoPatriarca(UsuarioGuidOrganizacaoPatriarca);
            InformacoesOrganizacao(despacho);
            InformacoesUnidade(despacho);
            InformacoesUsuario(despacho);

            repositorioDespachos.Add(despacho);
            unitOfWork.Save();

            return Pesquisar(despacho.Id);
        }

        private void PermissaoDespacho(DespachoModeloNegocio despacho)
        {
            List<int> listaIdsProcessosNaOrganizacao;
            processoNegocio.Usuario = Usuario;
            listaIdsProcessosNaOrganizacao = processoNegocio.PesquisarProcessoNaOrganizacao(UsuarioGuidOrganizacao.ToString("D")).Select(p => p.Id).ToList();

            if (!listaIdsProcessosNaOrganizacao.Contains(despacho.IdProcesso))
            {
                throw new RequisicaoInvalidaException("O processo não se encontra na organização do usuário. Não é possível realizar o despacho.");
            }
        }

        private void PreparaInsercaoDespacho(DespachoModeloNegocio despacho)
        {
           
            //Preenche processo dos anexos
            if (despacho.Anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in despacho.Anexos)
                {
                    anexo.IdProcesso = despacho.IdProcesso;
                }
            }

            //Data/hora atual do despacho
            despacho.DataHoraDespacho = DateTime.Now;
        }

        private void InformacoesOrganizacao(Despacho despacho)
        {
            OrganizacaoOrganogramaModelo organizacao = PesquisarOrganizacao(despacho.GuidOrganizacaoDestino);

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
            UnidadeOrganogramaModelo unidade = PesquisarUnidade(despacho.GuidUnidadeDestino);

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
            despacho.IdUsuarioDespachante = UsuarioCpf;
            despacho.NomeUsuarioDespachante = UsuarioNome;
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
