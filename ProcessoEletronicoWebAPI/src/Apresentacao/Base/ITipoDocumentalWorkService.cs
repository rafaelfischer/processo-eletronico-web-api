using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface ITipoDocumentalWorkService
    {
        List<TipoDocumentalModelo> Pesquisar(int idAtividade);
    }
}
