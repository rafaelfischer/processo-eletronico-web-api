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
            IdProcessoPreenchido(despacho);
            TextoPreenchido(despacho);
            GuidOrganizacaoDestinoPreenchido(despacho);
            GuidUnidadeDestinoPreenchido(despacho);
            
            /*Preenchimento de objetos associados ao processo*/
            anexoValidacao.Preenchido(despacho.Anexos);
        }

        private void IdProcessoPreenchido(DespachoModeloNegocio despacho)
        {
            if (despacho.IdProcesso <= 0)
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Identificador do processo não preenchido.");
            }
        }

        private void TextoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.Texto))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Texto do despacho não preenchido.");
            }
        }

        private void GuidOrganizacaoDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.GuidOrganizacaoDestino))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Identificador da organização de destino não preenchido.");
            }
        }
        
        private void GuidUnidadeDestinoPreenchido(DespachoModeloNegocio despacho)
        {
            if (string.IsNullOrWhiteSpace(despacho.GuidUnidadeDestino))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Identificador da unidade de destino não preenchido.");
            }
        }
        
        #endregion

        #region Validação dos campos

        public void Valido(int idAtividadeProcesso, DespachoModeloNegocio despacho, Guid guidOrganizacaoPatriarcaUsuario)
        {
            ProcessoExistente(despacho, guidOrganizacaoPatriarcaUsuario);
            GuidOrganizacaoDestinoValido(despacho);
            GuidUnidadeDestinoValido(despacho);
            anexoValidacao.Valido(despacho.Anexos, idAtividadeProcesso);
            anexoValidacao.TipoDocumentalExistente(despacho.Anexos, idAtividadeProcesso);
        }

        private void ProcessoExistente (DespachoModeloNegocio despacho, Guid guidOrganizacaoPatriarcaUsuario)
        {
            if (repositorioProcessos.Where(p => p.Id == despacho.IdProcesso 
                                             && p.OrganizacaoProcesso.GuidOrganizacao.Equals(guidOrganizacaoPatriarcaUsuario)).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Processo inexistente.");
            }
        }

        private void GuidOrganizacaoDestinoValido(DespachoModeloNegocio despacho)
        {
            try
            {
                Guid guid = new Guid(despacho.GuidOrganizacaoDestino);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("Identificador da Organização de destino inválido.");
            }
        }

        private void GuidUnidadeDestinoValido(DespachoModeloNegocio despacho)
        {
            try
            {
                Guid guid = new Guid(despacho.GuidUnidadeDestino);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("Identificador da Unidade de destino inválido.");
            }
        }

        #endregion

        #region Existência do despacho
        public void Existe (Despacho despacho)
        {
            if (despacho == null)
            {
                throw new RecursoNaoEncontradoException("Despacho não encontrado.");
            }
        }
        #endregion
    }
}
