using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Apresentacao
{
    public class TipoDocumentalWorkService : ITipoDocumentalWorkService
    {
        private ITipoDocumentalNegocio tipoDocumentalNegocio;

        public TipoDocumentalWorkService(ITipoDocumentalNegocio tipoDocumentalNegocio)
        {
            this.tipoDocumentalNegocio = tipoDocumentalNegocio;
        }
        public List<TipoDocumentalModelo> Pesquisar(int idAtividade)
        {
            List<TipoDocumentalModeloNegocio> tiposDocumentais = tipoDocumentalNegocio.Pesquisar(idAtividade);

            return Mapper.Map<List<TipoDocumentalModelo>>(tiposDocumentais);


        }
    }
}
