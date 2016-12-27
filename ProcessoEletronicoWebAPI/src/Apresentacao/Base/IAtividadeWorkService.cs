using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IAtividadeWorkService : IBaseWorkService
    {
        IEnumerable<AtividadeModelo> Pesquisar(int idFuncao);
        IEnumerable<AtividadeProcessoGetModelo> Pesquisar();
    }
}
