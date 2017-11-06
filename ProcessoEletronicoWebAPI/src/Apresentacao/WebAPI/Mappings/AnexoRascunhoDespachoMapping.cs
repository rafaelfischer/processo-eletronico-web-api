using Apresentacao.WebAPI.Models;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.WebAPI.Mappings
{
    public class AenxoRascunhoDespachoMapping : Profile
    {
        public AenxoRascunhoDespachoMapping()
        {
            CreateMap<PostRascunhoAnexoDto, AnexoRascunhoDespachoModel>()
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo));

            CreateMap<AnexoRascunhoDespachoModel, GetRascunhoAnexoDto>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.ConteudoString));
        }
    }
}
