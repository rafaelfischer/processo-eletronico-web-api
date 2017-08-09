using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Mapeamentos
{
    public class AnexoMapper : Profile
    {
        public AnexoMapper()
        {
            CreateMap<AnexoModeloNegocio, GetAnexoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.Conteudo != null ? Convert.ToBase64String(src.Conteudo) : null))
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental));

            CreateMap<PostAnexoDto, AnexoModeloNegocio>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.IdTipoDocumental.HasValue && src.IdTipoDocumental.Value > 0 ? new TipoDocumentalModeloNegocio { Id = src.IdTipoDocumental.Value} : null));

            CreateMap<PatchAnexoDto, AnexoModeloNegocio>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.IdTipoDocumental.HasValue && src.IdTipoDocumental.Value > 0 ? new TipoDocumentalModeloNegocio { Id = src.IdTipoDocumental.Value } : null));
            
            CreateMap<AnexoModeloNegocio, PatchAnexoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.Conteudo != null ? Convert.ToBase64String(src.Conteudo) : null))
                .ForMember(dest => dest.IdTipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental != null ? src.TipoDocumental.Id : (int?) null));
        }
    }
}
