using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IDestinacaoFinalNegocio : IBaseNegocio
    {
        List<DestinacaoFinalModeloNegocio> Listar();
    }
}
