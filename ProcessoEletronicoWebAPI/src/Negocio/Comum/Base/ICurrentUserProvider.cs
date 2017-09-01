using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum.Base
{
    public interface ICurrentUserProvider
    {
        string UserCpf { get; }
        string UserNome { get; }
        string UserSistema { get;  }
        Guid UserGuidOrganizacao { get; }
        Guid UserGuidOrganizacaoPatriarca { get; }
    }
}
