using AutoMapper;
using Negocio.Bloqueios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Bloqueios
{
    public class BloqueioMappings : Profile
    {
        public BloqueioMappings()
        {
            CreateMap<PostBloqueioDto, BloqueioModel>();
            CreateMap<BloqueioModel, GetBloqueioDto>()
                .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => src.DataInicio.ToString()))
                .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => src.DataFim.ToString()));
        }
    }
}
