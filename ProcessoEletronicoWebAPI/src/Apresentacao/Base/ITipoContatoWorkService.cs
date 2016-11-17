using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface ITipoContatoWorkService
    {
        IEnumerable<TipoContatoModelo> Listar();
    }
}
