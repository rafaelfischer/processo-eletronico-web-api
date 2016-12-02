using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class DespachoValidacao
    {
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<Despacho> repositorioDespachos;
        AnexoValidacao anexoValidacao;


        public DespachoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioProcessos = repositorios.Processos;
            this.repositorioDespachos = repositorios.Despachos;
            anexoValidacao = new AnexoValidacao(repositorios);
        }

        #region Preechimento dos campos obrigatórios

        public void Preenchido(DespachoModeloNegocio despacho)
        {
            /*Preenchimentos dos campos do processo*/
            TextoPreenchido(despacho);
            IdOrgaoDestinoPreenchido(despacho);
            NomeOrgaoDestinoPreenchido(despacho);
            SiglaOrgaoDestinoPreenchida(despacho);
            IdUnidadeDestinoPreenchido(despacho);
            NomeUnidadeDestinoPreenchido(despacho);
            SiglaUnidadeDestinoPreenchida(despacho);
            IdUsuarioDespachantePreenchido(despacho);
            NomeUsuarioDespachantePreenchido(despacho);
            
            /*Preenchimento de objetos associados ao processo*/
            anexoValidacao.Preenchido(despacho.Anexos);
        }

        private void TextoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.Texto))
            {
                throw new RequisicaoInvalidaException("Texto do despacho não preenchido.");
            }
        }

        private void IdOrgaoDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (despacho.IdOrgaoDestino == 0)
            {
                throw new RequisicaoInvalidaException("Identificador do órgao de destino não preenchido.");
            }
        }

        private void NomeOrgaoDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.NomeOrgaoDestino))
            {
                throw new RequisicaoInvalidaException("Nome do órgao de destino não preenchido.");
            }
        }

        private void SiglaOrgaoDestinoPreenchida(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.SiglaOrgaoDestino))
            {
                throw new RequisicaoInvalidaException("Sigla do órgao de destino não preenchida.");
            }
        }

        private void IdUnidadeDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (despacho.IdUnidadeDestino == 0)
            {
                throw new RequisicaoInvalidaException("Identificador da unidade de destino não preenchido.");
            }
        }

        private void NomeUnidadeDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.NomeUnidadeDestino))
            {
                throw new RequisicaoInvalidaException("Nome da unidade de destino não preenchido.");
            }
        }

        private void SiglaUnidadeDestinoPreenchida(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.SiglaUnidadeDestino))
            {
                throw new RequisicaoInvalidaException("Sigla da unidade de destino não preenchida.");
            }
        }

        private void IdUsuarioDespachantePreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.IdUsuarioDespachante))
            {
                throw new RequisicaoInvalidaException("Identificador do usuário despachante não preenchido.");
            }
        }

        private void NomeUsuarioDespachantePreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.NomeUsuarioDespachante))
            {
                throw new RequisicaoInvalidaException("Nome do usuário despachante não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos

        public void Valido(int idOrganizacaoProcesso, int idProcesso, int idAtividadeProcesso, DespachoModeloNegocio despacho)
        {
            ProcessoExistente(idOrganizacaoProcesso, idProcesso);
            anexoValidacao.Valido(despacho.Anexos, idAtividadeProcesso);
            anexoValidacao.TipoDocumentalExistente(despacho.Anexos, idAtividadeProcesso);
        }

        private void ProcessoExistente (int idOrganizacao, int idProcesso)
        {
            if (repositorioProcessos.Where(p => p.Id == idProcesso 
                                             && p.OrganizacaoProcesso.Id == idOrganizacao)
                                             .ToList().Count == 0)
            {
                throw new RecursoNaoEncontradoException("Processo inexistente.");

            }
        }

        #endregion

        #region Existência do despacho
        public void Existe (Despacho despacho)
        {
            if (despacho == null)
            {
                throw new RecursoNaoEncontradoException("Despacho não existe.");
            }
        }
        #endregion
    }
}
