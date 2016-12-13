using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IDespachoNegocio : IBaseNegocio
    {
        DespachoModeloNegocio PesquisarDespacho(int idDespacho);
        DespachoModeloNegocio Despachar(int idProcesso, DespachoModeloNegocio despachoNegocio);
        List<DespachoModeloNegocio> PesquisarDespachosUsuario();
    }
}
