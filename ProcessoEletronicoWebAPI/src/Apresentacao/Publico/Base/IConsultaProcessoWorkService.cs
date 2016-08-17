using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Publico.Base
{
    public interface IConsultaProcessoWorkService
    {
        string ConsultarPorNumero(string numeroProcesso);
    }
}
