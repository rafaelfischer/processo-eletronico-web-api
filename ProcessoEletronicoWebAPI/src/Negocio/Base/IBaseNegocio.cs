using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IBaseNegocio
    {
        Dictionary<string, string> Usuario { get; set; }
        string UsuarioCpf { get; }
        string UsuarioNome { get; }
        Guid UsuarioGuidOrganizacao { get; }
        Guid UsuarioGuidOrganizacaoPatriarca { get; }
    }
}
