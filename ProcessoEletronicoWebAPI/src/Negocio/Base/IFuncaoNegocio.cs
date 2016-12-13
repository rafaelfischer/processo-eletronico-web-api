using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IFuncaoNegocio
    {
        List<FuncaoModeloNegocio> Pesquisar(int idPlanoClassificacao);
    }
}
