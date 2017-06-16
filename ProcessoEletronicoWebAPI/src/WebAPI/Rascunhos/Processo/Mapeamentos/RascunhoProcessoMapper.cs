using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Mapeamentos
{
    public class RascunhoProcessoMapper : Profile
    {
        public RascunhoProcessoMapper()
        {
            CreateMap<PostRascunhoProcessoDto, RascunhoProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => src.IdAtividade.HasValue ? new AtividadeModeloNegocio { Id = src.IdAtividade.Value } : null))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaFisicaModeloNegocio>>(src.InteressadosPessoaFisica)))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaJuridicaModeloNegocio>>(src.InteressadosPessoaJuridica)))
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(src => src.IdSinalizacoes))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosRascunhoProcesso, opt => opt.MapFrom(src => src.MunicipiosRascunhoProcesso));

            CreateMap<PatchRascunhoProcessoDto, RascunhoProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => src.IdAtividade.HasValue ? new AtividadeModeloNegocio { Id = src.IdAtividade.Value } : null));
                
            CreateMap<RascunhoProcessoModeloNegocio, GetRascunhoProcessoPorOrganizacaoDto>()
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id));

            CreateMap<RascunhoProcessoModeloNegocio, GetRascunhoProcessoDto>()
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica != null && src.InteressadosPessoaFisica.Count > 0 ? src.InteressadosPessoaFisica : null))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica != null && src.InteressadosPessoaJuridica.Count > 0 ? src.InteressadosPessoaJuridica : null))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosProcesso, opt => opt.MapFrom(src => src.MunicipiosRascunhoProcesso));

            CreateMap<RascunhoProcessoModeloNegocio, PatchRascunhoProcessoDto>()
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade != null ? src.Atividade.Id : (int?)null));


            
        }
    }
}
