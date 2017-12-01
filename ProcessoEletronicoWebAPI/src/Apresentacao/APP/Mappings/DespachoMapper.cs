using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ProcessoEletronicoService.Negocio.Modelos;
using Apresentacao.APP.ViewModels;

namespace Apresentacao.APP.Mappings
{
    public class DespachoMapper : Profile
    {
        public DespachoMapper(){
            CreateMap<DespachoModeloNegocio, DespachoBasicoViewModel>();
        }
    }
}
