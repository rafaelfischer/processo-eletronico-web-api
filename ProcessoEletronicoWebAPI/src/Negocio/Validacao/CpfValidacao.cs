using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class CpfValidacao
    {

        public void CpfValido(string cpf)
        {

            if (cpf.Length != 11)
            {
                throw new RequisicaoInvalidaException("Cpf deve ser composto por 11 dígitos");
            }

            string soNumero = Regex.Replace(cpf, "[^0-9]", string.Empty);

            //Verifica se todos os numeros são iguais
            if (new string(soNumero[0], soNumero.Length) == soNumero)
            {
                throw new RequisicaoInvalidaException("Cpf inválido");
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();

            if (!cpf.EndsWith(digito))
            {
                throw new RequisicaoInvalidaException("Cpf inválido");
            }

        }
    }
}
