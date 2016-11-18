using System;
using AutoMapper;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Negocio.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static NegocioProfile GetNegocioProfile()
        {
            return new NegocioProfile();
        }
    }
    public class NegocioProfile : Profile
    {

        public NegocioProfile()
        {
            #region Mapeamento de assunto
            CreateMap<ProcessoRepositorio, Assunto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.assunto.id))
                .ForMember(dest => dest.descricao, opt => opt.MapFrom(src => src.assunto.descricao));
            #endregion

            #region Mapeamento de atividade
            CreateMap<Atividade, AtividadeModeloNegocio>()
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(s => s.Funcao));
            #endregion

            #region Mapeamento de função
            CreateMap<Funcao, FuncaoModeloNegocio>()
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.MapFrom(s => s.PlanoClassificacao))
                .ForMember(dest => dest.FuncaoPai, opt => opt.MapFrom(s => s.FuncaoPai != null ? Mapper.Map<Funcao, FuncaoModeloNegocio>(s.FuncaoPai) : null))
                .MaxDepth(1);
            #endregion

            #region Mapeamento de órgão
            CreateMap<ProcessoRepositorio, Orgao>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.orgaoAutuacao.id))
                .ForMember(dest => dest.sigla, opt => opt.MapFrom(src => src.orgaoAutuacao.sigla));
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { IdOrganizacao = src.IdOrganizacaoProcesso}));
            #endregion

            #region Mapeamento de processo
            CreateMap<ProcessoRepositorio, ProcessoNegocio>()
                .ForMember(dest => dest.numero, opt => opt.MapFrom(src => src.numero))
                .ForMember(dest => dest.digito, opt => opt.MapFrom(src => src.digito))
                .ForMember(dest => dest.resumo, opt => opt.MapFrom(src => src.resumo))
                .ForMember(dest => dest.dataAutuacao, opt => opt.MapFrom(src => src.dataAutuacao))
                .ForMember(dest => dest.assunto, opt => opt.MapFrom(s => Mapper.Map<ProcessoRepositorio, Assunto>(s)))
                .ForMember(dest => dest.orgaoAutuacao, opt => opt.MapFrom(s => Mapper.Map<ProcessoRepositorio, Orgao>(s)));
            #endregion

            #region Mapeamento de sinalização
            CreateMap<Sinalizacao, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { IdOrganizacao = src.IdOrganizacaoProcesso }))
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem == null ? null : src.Imagem))
                ;
            #endregion

        }

    }
}


