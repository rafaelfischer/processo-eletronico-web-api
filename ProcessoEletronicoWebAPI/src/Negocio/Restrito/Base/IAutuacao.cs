using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Restrito.Base
{
    public interface IAutuacao
    {
        string Autuar(int numeroProcesso);
    }
}
