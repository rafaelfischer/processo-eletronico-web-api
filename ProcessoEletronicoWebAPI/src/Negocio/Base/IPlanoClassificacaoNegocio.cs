using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IPlanoClassificacaoNegocio : IBaseNegocio
    {
        PlanoClassificacaoModeloNegocio Pesquisar(int id);
        List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao);
        PlanoClassificacaoModeloNegocio Inserir(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio);
        void Excluir(int id);
    }
}
