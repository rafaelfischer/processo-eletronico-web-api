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
            #region Mapeamento de atividade
            CreateMap<Atividade, AtividadeModeloNegocio>()
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(s => s.Funcao));
            #endregion

            #region Mapeamento de Destinação

            CreateMap<DestinacaoFinal, DestinacaoFinalModeloNegocio>();

            #endregion

            #region Mapeamento de função
            CreateMap<Funcao, FuncaoModeloNegocio>()
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.MapFrom(s => s.PlanoClassificacao))
                .ForMember(dest => dest.FuncaoPai, opt => opt.MapFrom(s => s.FuncaoPai != null ? Mapper.Map<Funcao, FuncaoModeloNegocio>(s.FuncaoPai) : null))
                .MaxDepth(1);
            #endregion

            #region Mapeamento de organização processo
            CreateMap<OrganizacaoProcesso, OrganizacaoProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { IdOrganizacao = src.IdOrganizacaoProcesso}));
            #endregion

            #region Mapeamento de processo
            CreateMap<Processo, ProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de tipo de contato

            CreateMap<TipoContato, TipoContatoModeloNegocio>();

            #endregion

            #region Mapeamento de Tipo de Documento

            CreateMap<TipoDocumental, TipoDocumentalModeloNegocio>()
                .ForMember(dest => dest.PrazoGuardaSubjetivoCorrente, opt => opt.MapFrom(src => src.PrazoGuardaSubjetivoCorrente))
                .ForMember(dest => dest.PrazoGuardaSubjetivoIntermediaria, opt => opt.MapFrom(src => src.PrazoGuardaSubjetivoIntermediaria))
                .ForMember(dest => dest.DestinacaoFinal, opt => opt.MapFrom(src => src.DestinacaoFinal))
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => src.Atividade));

            #endregion

            #region Mapeamento de Prazo de Guarda

            CreateMap<PrazoGuardaSubjetivo, PrazoGuardaSubjetivoModeloNegocio>();
            
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


