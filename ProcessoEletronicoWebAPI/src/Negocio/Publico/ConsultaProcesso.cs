using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Negocio.Publico.Base;

namespace ProcessoEletronicoService.Negocio.Publico
{
    public class ConsultaProcesso : IConsultaProcesso
    {
        public string ConsultarPorNumero(string numeroProcesso)
        {
            return "Negócio - Consulta de Processo por Número. Processo número " + numeroProcesso;
        }
    }
}
