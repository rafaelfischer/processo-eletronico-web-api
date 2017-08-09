using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
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
            CreateMap<ContatoModeloNegocio, GetContatoDto>()
                .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src => src.TipoContato));
            CreateMap<PostContatoDto, ContatoModeloNegocio>()
                .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src => src.IdTipoContato.HasValue ? new TipoContatoModeloNegocio { Id = src.IdTipoContato.Value } : null));
            CreateMap<ContatoModeloNegocio, PatchContatoDto>();
            CreateMap<PatchContatoDto, ContatoModeloNegocio>().
                ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src => src.IdTipoContato.HasValue ? new TipoContatoModeloNegocio { Id = src.IdTipoContato.Value } : null));
        }
    }
}
