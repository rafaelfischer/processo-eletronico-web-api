using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class EmailValidacao : IBaseValidation<EmailModeloNegocio, EmailRascunho>, IBaseCollectionValidation<EmailModeloNegocio>
    {
        private string patternEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public void Exists(EmailRascunho email)
        {
            if (email == null)
            {
                throw new RecursoNaoEncontradoException("Email não encontrado");
            }
        }

        public void IsFilled(IEnumerable<EmailModeloNegocio> emailsModeloNegocio)
        {
            if (emailsModeloNegocio != null)
            {
                foreach (EmailModeloNegocio emailModeloNegocio in emailsModeloNegocio)
                {
                    IsFilled(emailModeloNegocio);
                }
            }
        }

        public void IsFilled(EmailModeloNegocio emailModeloNegocio)
        {
        }

        public void IsValid(IEnumerable<EmailModeloNegocio> emailsModeloNegocio)
        {
            if (emailsModeloNegocio != null)
            {
                foreach (EmailModeloNegocio emailModeloNegocio in emailsModeloNegocio)
                {
                    IsValid(emailModeloNegocio);
                }
            }
        }

        public void IsValid(EmailModeloNegocio emailModeloNegocio)
        {
            EnderecoIsValid(emailModeloNegocio);
        }

        private void EnderecoIsValid(EmailModeloNegocio emailModeloNegocio)
        {
            if (!string.IsNullOrWhiteSpace(emailModeloNegocio.Endereco))
            {
                Regex regexEmail = new Regex(patternEmail);
                if (!regexEmail.IsMatch(emailModeloNegocio.Endereco))
                {
                    throw new RequisicaoInvalidaException($"Email {emailModeloNegocio.Endereco} inválido");
                }
            }
        }
        
    }
}
