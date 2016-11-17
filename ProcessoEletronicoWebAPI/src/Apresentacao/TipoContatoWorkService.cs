using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;

namespace ProcessoEletronicoService.Apresentacao
{
    public class TipoContatoWorkService : ITipoContatoWorkService
    {
        ITipoContatoNegocio tipoContatoNegocio;

        public TipoContatoWorkService(ITipoContatoNegocio tipoContatoNegocio)
        {
            this.tipoContatoNegocio = tipoContatoNegocio;
        }

        public IEnumerable<TipoContatoModelo> Listar()
        {
            List<TipoContatoModeloNegocio> tiposContato = tipoContatoNegocio.Listar();

            return Mapper.Map<List<TipoContatoModeloNegocio>, List<TipoContatoModelo>>(tiposContato);

        }
    }
}
