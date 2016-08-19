using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Base
{
    public interface IConsultaProcessoRepositorio
    {
        string ConsultarProcessoPorNumero(string numeroProcesso);


    }
}
