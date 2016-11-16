using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IFuncaoWorkService
    {
        IEnumerable<FuncaoModelo> Pesquisar(int idOrganizacaoPatriarca, int idPlanoClassificacao);
    }
}
