using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Sinalizacoes.Base
{
    public interface ISinalizacaoNegocio
    {
        SinalizacaoModeloNegocio Get(int id);
        IList<SinalizacaoModeloNegocio> Get(string guidOrganizacaoPatriarca);
        IList<SinalizacaoModeloNegocio> Get();
        SinalizacaoModeloNegocio Add(SinalizacaoModeloNegocio sinalizacaoModeloNegocio);
        void Update(int id, SinalizacaoModeloNegocio sinalizacaoModeloNegocio);
        void Delete(int id);
    }
}
