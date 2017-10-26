using AutoMapper;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using System;

namespace Negocio.RascunhosDespacho.Mapping
{
    public class RascunhoDespachoMapping : Profile
    {
        public RascunhoDespachoMapping()
        {
            CreateMap<RascunhoDespachoModel, RascunhoDespacho>()
                .ForMember(dest => dest.GuidOrganizacaoDestino, opt => opt.MapFrom(src => new Guid(src.GuidOrganizacaoDestino)))
                .ForMember(dest => dest.GuidUnidadeDestino, opt => opt.MapFrom(src => new Guid(src.GuidUnidadeDestino)))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.Ignore())
                .ForMember(dest => dest.AnexosRascunho, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<RascunhoDespacho, RascunhoDespachoModel>()
                .ForMember(dest => dest.GuidOrganizacaoDestino, opt => opt.MapFrom(src => src.GuidOrganizacaoDestino.HasValue ? src.GuidOrganizacaoDestino.Value.ToString("D") : null))
                .ForMember(dest => dest.GuidUnidadeDestino, opt => opt.MapFrom(src => src.GuidUnidadeDestino.HasValue ? src.GuidUnidadeDestino.Value.ToString("D") : null))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => src.GuidOrganizacao.ToString("D")))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.AnexosRascunho));
        }
    }
}
