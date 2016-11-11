using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IPlanoClassificacaoWorkService
    {
        IEnumerable<PlanoClassificacaoModelo> Pesquisar(int idOrganizacaoPatriarca, int idOrganizacao);
    }
}
