using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IFuncaoWorkService : IBaseWorkService
    {
        IEnumerable<FuncaoModelo> Pesquisar(int idPlanoClassificacao);
        FuncaoProcessoGetModelo Inserir(FuncaoModeloPost funcao);
        void Excluir(int id);
    }
}
