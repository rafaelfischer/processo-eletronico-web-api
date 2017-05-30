using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Mapeamentos
{
    public class TipoContatoMapper : Profile
    {
        public TipoContatoMapper()
        {
            CreateMap<TipoContatoModeloNegocio, GetTipoContatoDto>();
        }
    }
}
