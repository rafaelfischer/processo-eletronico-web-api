using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IFuncaoNegocio : IBaseNegocio
    {
        List<FuncaoModeloNegocio> PesquisarPorPlanoClassificacao(int idPlanoClassificacao);
        FuncaoModeloNegocio Pesquisar(int id);
        FuncaoModeloNegocio Inserir(FuncaoModeloNegocio funcaoModeloNegocio);
        void Excluir(int id);
    }
}
