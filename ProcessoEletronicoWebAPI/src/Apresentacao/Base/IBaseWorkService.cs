using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IBaseWorkService
    {
        Dictionary<string, string> Usuario { get; set; }
        void RaiseUsuarioAlterado();
    }
}
