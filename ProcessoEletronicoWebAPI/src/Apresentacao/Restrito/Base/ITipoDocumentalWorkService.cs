using ProcessoEletronicoService.Apresentacao.Restrito.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Restrito.Base
{
    public interface ITipoDocumentalWorkService
    {
        List<TipoDocumentalModelo> ObterTiposDocumentais();

        TipoDocumentalModelo ObterTiposDocumentais(int id);

        TipoDocumentalModelo Incluir(TipoDocumentalModelo tipoDocumental);

        void Alterar(int id, TipoDocumentalModelo tipoDocumental);

        void Excluir(int id);
    }
}
