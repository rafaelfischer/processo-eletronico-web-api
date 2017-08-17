using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class AnexoMapper : Profile
    {
        public AnexoMapper()
        {
            CreateMap<AnexoRascunho, AnexoModeloNegocio>()
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental))
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => Convert.ToBase64String(src.Conteudo)));


            CreateMap<AnexoModeloNegocio, AnexoRascunho>()
                .ForMember(dest => dest.TipoDocumental, opt => opt.Ignore())
                .ForMember(dest => dest.IdTipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental != null ? src.TipoDocumental.Id : (int?)null));
        }
    }
}
