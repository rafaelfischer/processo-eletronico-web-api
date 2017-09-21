using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Config
{
    public class CurrentUser : ICurrentUserProvider
    {
        public string UserCpf => throw new NotImplementedException();

        public string UserNome => throw new NotImplementedException();

        public string UserSistema { get; set; } = "processoeletronico";

        public Guid UserGuidOrganizacao => throw new NotImplementedException();

        public Guid UserGuidOrganizacaoPatriarca => throw new NotImplementedException();
    }
}
