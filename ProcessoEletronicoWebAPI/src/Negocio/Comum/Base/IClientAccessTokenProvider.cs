using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum.Base
{
    public interface IClientAccessTokenProvider
    {
        string AccessToken { get; }
    }
}
