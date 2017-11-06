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
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => Convert.FromBase64String(src.ConteudoString)));

            CreateMap<AnexoRascunho, AnexoRascunhoDespachoModel>();
        }
    }
}
