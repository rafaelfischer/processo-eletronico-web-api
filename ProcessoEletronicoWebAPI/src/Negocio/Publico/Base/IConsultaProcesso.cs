using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Publico.Base
{
    public interface IConsultaProcesso
    {
        string ConsultarPorNumero(string numeroProcesso);
    }
}
