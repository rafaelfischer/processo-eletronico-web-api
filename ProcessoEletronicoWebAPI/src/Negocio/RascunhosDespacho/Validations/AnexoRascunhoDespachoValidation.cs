using Negocio.RascunhosDespacho.Validations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace Negocio.RascunhosDespacho.Validations
{
    public class AnexoRascunhoDespachoValidation : IAnexoRascunhoDespachoValidation
    {
        public void Exists(AnexoRascunho anexoRascunho)
        {
            if (anexoRascunho == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void IsFilled(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (anexoRascunhoDespachoModel == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void IsFilled(IEnumerable<AnexoRascunhoDespachoModel> anexosRascunhoDespacho)
        {
        }

        public void IsValid(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (anexoRascunhoDespachoModel != null)
            {
                ConteudoIsValid(anexoRascunhoDespachoModel);
            }
        }
        
        public void IsValid(IEnumerable<AnexoRascunhoDespachoModel> anexosRascunhoDespachoModel)
        {
            if (anexosRascunhoDespachoModel != null)
            {
                foreach (AnexoRascunhoDespachoModel anexoRascunhoDespachoModel in anexosRascunhoDespachoModel)
                {
                    IsValid(anexoRascunhoDespachoModel);
                }
            }
        }

        private void ConteudoIsValid(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(anexoRascunhoDespachoModel.ConteudoString))
            {
                try
                {
                    anexoRascunhoDespachoModel.Conteudo = Convert.FromBase64String(anexoRascunhoDespachoModel.ConteudoString);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Conteúdo do anexo " + anexoRascunhoDespachoModel.Nome + " inválido.");
                }
            }
        }
    }
}
