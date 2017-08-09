using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IDestinacaoFinalNegocio : IBaseNegocio
    {
        List<DestinacaoFinalModeloNegocio> Listar();
        DestinacaoFinalModeloNegocio Pesquisar(int id);
        DestinacaoFinalModeloNegocio Inserir(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio);
        void Excluir(int id);
    }
}
