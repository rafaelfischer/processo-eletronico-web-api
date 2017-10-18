using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Mappings
{
    public class ProcessoMapper : Profile
    {
        public ProcessoMapper()
        {
            CreateMap<ProcessoModeloNegocio, GetProcessoViewModel>();
            CreateMap<AtividadeModeloNegocio, AtividadeViewModel>();
            CreateMap<RascunhoProcessoModeloNegocio, GetRascunhoProcessoViewModel>();
            CreateMap<AtividadeModeloNegocio, AtividadeViewModel>();
            CreateMap<Unidade, UnidadeViewModel>();
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoViewModel>();
            CreateMap<Organizacao, OrganizacaoViewModel>();
            
        }
    }
}
