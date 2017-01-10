using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IFuncaoWorkService : IBaseWorkService
    {
        IEnumerable<FuncaoModelo> PesquisarPorPlanoClassificacao(int idPlanoClassificacao);
        FuncaoProcessoGetModelo Pesquisar(int id);
        FuncaoProcessoGetModelo Inserir(FuncaoModeloPost funcao);
        void Excluir(int id);
    }
}
