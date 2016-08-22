using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Restrito.Base
{
    public interface ITipoDocumentalNegocio
    {
        List<TipoDocumentalModeloNegocio> ObterTiposDocumentais();

        TipoDocumentalModeloNegocio ObterTiposDocumentais(int id);

        TipoDocumentalModeloNegocio Incluir(TipoDocumentalModeloNegocio tipoDocumental);

        void Alterar(int id, TipoDocumentalModeloNegocio tipoDocumental);

        void Excluir(int id);
    }
}
