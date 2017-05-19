using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IDespachoNegocio : IBaseNegocio
    {
        DespachoModeloNegocio Pesquisar(int idDespacho);
        DespachoModeloNegocio Despachar(DespachoModeloNegocio despachoNegocio);
        List<DespachoModeloNegocio> PesquisarDespachosUsuario();
    }
}
