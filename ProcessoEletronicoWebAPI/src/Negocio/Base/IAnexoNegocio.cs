using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IAnexoNegocio
    {
        AnexoModeloNegocio Pesquisar(int id);
        
    }
}
