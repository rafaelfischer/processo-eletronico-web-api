using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class RascunhoProcessoMapper : Profile
    {
        public RascunhoProcessoMapper()
        {
            CreateMap<RascunhoProcessoModeloNegocio, RascunhoProcesso>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.Atividade, opt => opt.Ignore())
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica))
                .ForMember(dest => dest.MunicipiosRascunhoProcesso, opt => opt.MapFrom(src => src.MunicipiosRascunhoProcesso))
                .ForMember(dest => dest.SinalizacoesRascunhoProcesso, opt => opt.MapFrom(src => src.Sinalizacoes))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => new Guid(src.GuidOrganizacao)))
                .ForMember(dest => dest.GuidUnidade, opt => opt.MapFrom(src => new Guid(src.GuidUnidade)))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.Ignore());

            CreateMap<RascunhoProcesso, RascunhoProcessoModeloNegocio>()
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(s => s.SinalizacoesRascunhoProcesso))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica.ToList()))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica.ToList()))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosRascunhoProcesso, opt => opt.MapFrom(src => src.MunicipiosRascunhoProcesso))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => src.GuidOrganizacao.ToString("D")))
                .ForMember(dest => dest.GuidUnidade, opt => opt.MapFrom(src => src.GuidUnidade.ToString("D")))
                .MaxDepth(1);



        }
    }
}
