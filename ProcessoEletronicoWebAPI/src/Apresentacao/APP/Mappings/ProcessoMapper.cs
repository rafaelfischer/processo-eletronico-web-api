using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
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
        }
    }
}
