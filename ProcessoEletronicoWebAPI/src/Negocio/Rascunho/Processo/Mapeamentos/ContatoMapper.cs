using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class ContatoMapper : Profile
    {
        public ContatoMapper()
        {
            CreateMap<ContatoModeloNegocio, ContatoRascunho>()
                .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(src => src.TipoContato != null ? src.TipoContato.Id : (int?)null))
                .ForMember(dest => dest.TipoContato, opt => opt.Ignore());
            CreateMap<ContatoRascunho, ContatoModeloNegocio>()
                .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src => src.TipoContato));
        }
    }
}
