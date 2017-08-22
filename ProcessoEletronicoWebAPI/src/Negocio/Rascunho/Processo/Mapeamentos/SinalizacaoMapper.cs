using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class SinalizacaoMapper : Profile
    {
        public SinalizacaoMapper()
        {
            CreateMap<SinalizacaoRascunhoProcesso, SinalizacaoModeloNegocio>()
                .ConvertUsing(s => s.Sinalizacao != null ? Mapper.Map<Sinalizacao, SinalizacaoModeloNegocio>(s.Sinalizacao) : null);

            CreateMap<SinalizacaoModeloNegocio, SinalizacaoRascunhoProcesso>()
                .ForMember(dest => dest.IdSinalizacao, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
