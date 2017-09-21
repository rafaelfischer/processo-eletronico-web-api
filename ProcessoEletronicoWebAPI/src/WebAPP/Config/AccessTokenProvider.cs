using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Config
{
    public class AccessTokenProvider : IClientAccessTokenProvider
    {
        public string AccessToken => throw new NotImplementedException();
    }
}
