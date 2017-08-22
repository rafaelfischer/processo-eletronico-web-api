using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface ISinalizacaoNegocio
    {
        List<SinalizacaoModeloNegocio> Pesquisar(string guidOrganizacaoPatriarca);
    }
}