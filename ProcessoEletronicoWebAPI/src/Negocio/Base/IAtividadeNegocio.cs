using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IAtividadeNegocio
    {
        List<AtividadeModeloNegocio> Pesquisar(int idFuncao);
    }
}
