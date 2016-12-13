using ProcessoEletronicoService.Apresentacao.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface ISinalizacaoWorkService
    {
        List<SinalizacaoModelo> Pesquisar(string guidOrganizacaoPatriarca);
    }
}
