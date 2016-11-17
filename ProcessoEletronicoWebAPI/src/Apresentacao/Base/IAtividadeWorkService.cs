using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IAtividadeWorkService
    {
        IEnumerable<AtividadeModelo> Pesquisar(int idOrganizacaoPatriarca, int idFuncao);
    }
}
