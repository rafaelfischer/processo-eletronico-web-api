using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IPlanoClassificacaoWorkService : IBaseWorkService
    {
        IEnumerable<PlanoClassificacaoModelo> Pesquisar(string guidOrganizacao);
    }
}
