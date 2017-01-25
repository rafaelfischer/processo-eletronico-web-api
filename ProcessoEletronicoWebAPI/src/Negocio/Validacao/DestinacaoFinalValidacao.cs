using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class DestinacaoFinalValidacao
    {
        IRepositorioGenerico<DestinacaoFinal> repositorioDestinacoesFinais;
        IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;

        public DestinacaoFinalValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioDestinacoesFinais = repositorios.DestinacoesFinais;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
            
        }
        
        internal void Preenchido(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            DescricaoPreenchida(destinacaoFinalModeloNegocio);
        }
        
        internal void NaoEncontrado(DestinacaoFinal destinacaoFinal)
        {
            if (destinacaoFinal == null)
            {
                throw new RecursoNaoEncontradoException("Destinação final não encontrada.");
            }
        }
        
        private void DescricaoPreenchida(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(destinacaoFinalModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não preenchida.");
            }
        }
        
        
        internal void Valido(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            DescricaoExistente(destinacaoFinalModeloNegocio);
        }

        
        private void DescricaoExistente(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            if (repositorioDestinacoesFinais.Where(df => df.Descricao.ToUpper().Equals(destinacaoFinalModeloNegocio.Descricao.ToUpper())).SingleOrDefault() != null) {
                throw new RequisicaoInvalidaException("Já existe uma destinação final com essa descrição.");
            }
        }
        

        internal void PossivelExcluir(int id)
        {
            Existe(id);
            ExistemTiposDocumentais(id);
        }

        private void Existe(int id)
        {
            if (repositorioDestinacoesFinais.Where(df => df.Id == id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Destinação Final não encontrada.");
            }
        }

       
        private void ExistemTiposDocumentais(int id)
        {
            if (repositorioTiposDocumentais.Where(td => td.IdDestinacaoFinal == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem tipos documentais associados a essa destinação final.");
            }
        }
        
    }
}
