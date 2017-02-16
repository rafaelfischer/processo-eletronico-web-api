using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IPlanoClassificacaoWorkService : IBaseWorkService
    {
        PlanoClassificacaoProcessoGetModelo Pesquisar(int id);
        IEnumerable<PlanoClassificacaoModelo> Pesquisar(string guidOrganizacao);
        IEnumerable<PlanoClassificacaoModelo> Pesquisar();
        PlanoClassificacaoProcessoGetModelo Inserir(PlanoClassificacaoModeloPost planoClassificacao);
        void Excluir(int id);
    }
}
