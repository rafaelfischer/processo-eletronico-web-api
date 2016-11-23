using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class EmailValidacao
    {
        private string padraoEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public void Preenchido(List<EmailModeloNegocio> emails)
        {
            foreach (EmailModeloNegocio email in emails)
            {
                Preenchido(email);
            }
        }

        public void Preenchido(EmailModeloNegocio email)
        {
            if (email != null)
            {
                EnderecoPreenchido(email);
            }
        }

        #region Preenchimento de campos obrigatórios

        internal void EnderecoPreenchido(EmailModeloNegocio email)
        {
            if (string.IsNullOrWhiteSpace(email.Endereco))
            {
                throw new RequisicaoInvalidaException("Endereço do e-mail do interessado não preenchido.");
            }
        }

        internal void Valido(List<EmailModeloNegocio> emails)
        {
            foreach (EmailModeloNegocio email in emails)
            {
                Valido(email);
            }

            Repetido(emails);
        }

        internal void Valido(EmailModeloNegocio email)
        {
            if (email != null)
            {
                Regex emailRegex = new Regex(padraoEmail);

                if (!emailRegex.IsMatch(email.Endereco))
                {
                    throw new RequisicaoInvalidaException("Email inválido.");
                }
            }
        }

        private void Repetido(List<EmailModeloNegocio> emails)
        {
            var duplicados = emails.GroupBy(e => e.Endereco)
                                   .Where(g => g.Count() > 1)
                                   .Select(g => g.Key)
                                   .ToList();


            if (duplicados != null && duplicados.Count > 0)
            {
                throw new RequisicaoInvalidaException("Existe email duplicado.");
            }
        }

        #endregion

    }
}
