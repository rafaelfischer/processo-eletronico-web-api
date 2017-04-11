using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Base
{
    public interface IClientAccessToken
    {
        string AccessToken { get; }
    }
}
