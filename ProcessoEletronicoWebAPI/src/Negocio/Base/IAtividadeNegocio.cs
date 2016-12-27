using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IAtividadeNegocio : IBaseNegocio
    {
        List<AtividadeModeloNegocio> Pesquisar(int idFuncao);
        List<AtividadeModeloNegocio> Pesquisar();
    }
}
