using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public class AutenticacaoIdentityServer
    {
        public string Authority { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        //public string ScopeName { get; set; }
        public List<string> AllowedScopes { get; set; }
        public bool AutomaticAuthenticate { get; set; }
    }
}
