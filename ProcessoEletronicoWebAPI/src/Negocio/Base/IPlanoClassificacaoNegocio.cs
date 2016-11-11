using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IPlanoClassificacaoNegocio
    {
        List<PlanoClassificacaoModeloNegocio> Pesquisar(int idOrganizacaoPatriarca, int idOrganizacao);
    }
}
