using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IPlanoClassificacaoNegocio : IBaseNegocio
    {
        List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao);
    }
}
