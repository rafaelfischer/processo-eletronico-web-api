using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface ITipoDocumentalNegocio
    {
        List<TipoDocumentalModeloNegocio> Listar(int idOrganizacaoPatriarca, int idAtividade);

    }
}
