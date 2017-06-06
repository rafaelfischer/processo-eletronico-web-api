﻿using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.WebAPI.Mapeamentos
{
    public class InteressadoPessoaFisicaMapper : Profile
    {
        public InteressadoPessoaFisicaMapper()
        {
            CreateMap<PostInteressadoPessoaFisicaDto, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<PatchInteressadoPessoaFisicaDto, InteressadoPessoaFisicaModeloNegocio>().ReverseMap();

            CreateMap<InteressadoPessoaFisicaModeloNegocio, GetInteressadoPessoaFisicaDto>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));

            CreateMap<InteressadoPessoaFisicaModeloNegocio, GetInteressadoPessoaFisicaDto>()
               .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
               .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));
        }
    }
}