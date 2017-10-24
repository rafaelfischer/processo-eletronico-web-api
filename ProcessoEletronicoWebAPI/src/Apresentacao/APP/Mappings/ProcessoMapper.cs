using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apresentacao.APP.Mappings
{
    public class ProcessoMapper : Profile
    {
        public ProcessoMapper()
        {
            CreateMap<ProcessoModeloNegocio, GetProcessoViewModel>();
            CreateMap<AtividadeModeloNegocio, AtividadeViewModel>().ReverseMap();            
            CreateMap<RascunhoProcessoModeloNegocio, GetRascunhoProcessoViewModel>().ReverseMap();
            CreateMap<Unidade, UnidadeViewModel>().ReverseMap();
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoViewModel>().ReverseMap();
            CreateMap<Organizacao, OrganizacaoViewModel>();

            CreateMap<MunicipioProcessoModeloNegocio, MunicipioViewModel>();
            CreateMap<Municipio, MunicipioViewModel>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.Guid));


            CreateMap<RascunhoProcessoModeloNegocio, RascunhoProcessoViewModel>().ReverseMap();                        
                


        }
    }
}
