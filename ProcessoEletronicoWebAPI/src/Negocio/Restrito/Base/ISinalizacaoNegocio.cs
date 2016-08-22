using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Restrito.Base
{
    public interface ISinalizacaoNegocio
    {
        List<SinalizacaoModeloNegocio> Obter();

        SinalizacaoModeloNegocio Obter(int id);

        SinalizacaoModeloNegocio Incluir(SinalizacaoModeloNegocio sinalizacao);

        void Alterar(int id, SinalizacaoModeloNegocio sinalizacao);

        void Excluir(int id);
    }
}
