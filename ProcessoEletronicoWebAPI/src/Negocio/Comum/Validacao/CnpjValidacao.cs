using System;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Comum.Validacao
{
    public class CnpjValidacao
    {

        internal void CnpjValido(string cnpj)
        {

            if (!string.IsNullOrEmpty(cnpj))
            {

                if (cnpj.Length != 14)
                {
                    throw new RequisicaoInvalidaException("O Cnpj deve ser composto por 14 dígitos");
                }
                else
                {

                    try
                    {
                        long.Parse(cnpj);
                    }
                    catch (Exception)
                    {
                        throw new RequisicaoInvalidaException("O Cnpj deve ser composto apenas por números");
                    }

                    int[] digitos = new int[14];
                    int[] verificadores = new int[2];
                    int j, i, soma;
                    string sequencia, soNumero;
                    soNumero = Regex.Replace(cnpj, "[^0-9]", string.Empty);

                    //Verifica se todos os numeros são iguais
                    if (new string(soNumero[0], soNumero.Length) == soNumero)
                    {
                        throw new RequisicaoInvalidaException("O Cnpj informado é inválido");
                    }

                    sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++)
                    {
                        digitos[i] = Convert.ToInt32(soNumero.Substring(i, 1));
                    }

                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                        {
                            soma += digitos[j] * Convert.ToInt32(sequencia.Substring(j + 1 - i, 1));
                        }

                        verificadores[i] = (soma * 10) % 11;

                        if (verificadores[i] == 10)
                        {
                            verificadores[i] = 0;
                        }
                    }
                    if (verificadores[0] != digitos[12] || verificadores[1] != digitos[13])
                    {
                        throw new RequisicaoInvalidaException("O Cnpj informado é inválido");
                    }

                }

            }
        }

    }
}
