using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Sinalizacoes.Mapeamentos
{
    class SinalizacoesMapper : Profile
    {
        public SinalizacoesMapper()
        {
            CreateMap<Sinalizacao, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem ?? null))
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.Ignore())
                .ForMember(dest => dest.ImagemBase64String, opt => opt.MapFrom(src => src.Imagem != null ? Convert.ToBase64String(src.Imagem) : null))
                .MaxDepth(1);

            CreateMap<SinalizacaoModeloNegocio, Sinalizacao>()
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImagemBase64String) ? Convert.FromBase64String(src.ImagemBase64String) : null))
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.Ignore());

        }
    }
}
