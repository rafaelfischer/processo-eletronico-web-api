using AutoMapper;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using System;

namespace Negocio.RascunhosDespacho.Mapping
{
    public class AenxoRascunhoDespachoMapping : Profile
    {
        public AenxoRascunhoDespachoMapping()
        {
            CreateMap<AnexoRascunhoDespachoModel, AnexoRascunho>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => Convert.FromBase64String(src.ConteudoString)))
                .ForMember(dest => dest.IdTipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental != null ? src.TipoDocumental.Id : (int?) null));

            CreateMap<AnexoRascunho, AnexoRascunhoDespachoModel>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => Convert.ToBase64String(src.Conteudo)));
        }
    }
}
