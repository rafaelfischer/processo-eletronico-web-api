using Apresentacao.APP.ViewModels;
using AutoMapper;
using Negocio.RascunhosDespacho.Models;

namespace Apresentacao.APP.Mappings
{
    public class RascunhoDespachoMapper : Profile
    {
        public RascunhoDespachoMapper()
        {   
            CreateMap<RascunhoDespachoModel, RascunhoDespachoViewModel>().ReverseMap();
            CreateMap<RascunhoDespachoViewModel, RascunhoDespachoPatchModel>();
            CreateMap<AnexoRascunhoDespachoModel, AnexoRascunhoDespachoViewModel>().ReverseMap();
        }
    }
}
