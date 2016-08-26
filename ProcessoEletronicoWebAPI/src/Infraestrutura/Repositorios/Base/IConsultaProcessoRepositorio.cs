using Nest;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Base
{
    public interface IConsultaProcessoRepositorio
    {
        ProcessoRepositorio ConsultarProcessoPorNumero(string numeroProcesso);
    }
}
