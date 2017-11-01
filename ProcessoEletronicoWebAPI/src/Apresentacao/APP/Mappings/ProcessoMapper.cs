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
            /*Processo*/
            CreateMap<ProcessoModeloNegocio, GetProcessoViewModel>();
            CreateMap<RascunhoProcessoModeloNegocio, RascunhoProcessoViewModel>().ReverseMap();
            CreateMap<RascunhoProcessoModeloNegocio, GetRascunhoProcessoViewModel>().ReverseMap();

            CreateMap<AtividadeModeloNegocio, AtividadeViewModel>().ReverseMap();
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoViewModel>().ReverseMap();

            /*Organograma*/
            CreateMap<Unidade, UnidadeViewModel>().ReverseMap();            
            CreateMap<Organizacao, OrganizacaoViewModel>();

            CreateMap<MunicipioProcessoModeloNegocio, MunicipioViewModel>();
            CreateMap<MunicipioViewModel, MunicipioProcessoModeloNegocio>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Municipio, MunicipioViewModel>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.Guid))
                .ReverseMap();

            /*Anexos*/
            CreateMap<AnexoModeloNegocio, AnexoViewModel>().ReverseMap();
            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalViewModel>().ReverseMap();

            /*Interessados*/
            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisicaViewModel>().ReverseMap();
            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaViewModel>().ReverseMap();
            CreateMap<OrganizacaoViewModel, InteressadoPessoaJuridicaViewModel>().ReverseMap();
            CreateMap<OrganizacaoViewModel, InteressadoPessoaJuridicaModeloNegocio>().ReverseMap();

            CreateMap<TipoContatoModeloNegocio, TipoContatoViewModel>().ReverseMap();
            CreateMap<EmailModeloNegocio, EmailViewModel>().ReverseMap();
            CreateMap<ContatoModeloNegocio, ContatoViewModel>().ReverseMap();
        }
    }
}
