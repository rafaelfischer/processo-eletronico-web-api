using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IAnexoWorkService : IBaseWorkService
    {
        AnexoModeloGet Pesquisar(int id);
    }
}
