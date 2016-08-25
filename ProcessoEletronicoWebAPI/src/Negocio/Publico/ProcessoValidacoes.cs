using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Publico
{
    
    public static class ProcessoValidacoes
    {
        public static bool ValidarNumeroProcesso(string numeroProcesso)
        {
            //numeroProcesso deve possuir apenas número e ter entre 2 e 8 dígitos.

            int numero;
            if (int.TryParse(numeroProcesso, out numero) && (numeroProcesso.Length >= 2 && numeroProcesso.Length <= 8))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
