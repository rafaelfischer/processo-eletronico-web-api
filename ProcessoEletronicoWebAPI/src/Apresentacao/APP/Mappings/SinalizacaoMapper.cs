using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Mappings
{
    public class SinalizacaoMapper : Profile
    {
        public SinalizacaoMapper()
        {
            CreateMap<SinalizacaoViewModel, SinalizacaoModeloNegocio>().ReverseMap();
        }

    }
}
