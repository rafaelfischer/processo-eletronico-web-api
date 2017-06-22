using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class MunicipioMapper : Profile
    {
        public MunicipioMapper()
        {
            CreateMap<MunicipioRascunhoProcessoModeloNegocio, GetMunicipioDto>();
            CreateMap<PostMunicipioDto, MunicipioRascunhoProcessoModeloNegocio>();
            CreateMap<PatchMunicipioDto, MunicipioRascunhoProcessoModeloNegocio>().ReverseMap();
        }
    }
}
