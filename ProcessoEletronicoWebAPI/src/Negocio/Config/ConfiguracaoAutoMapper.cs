using System;
using AutoMapper;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;
using System.Linq;

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
            #region Mapeamento de anexo
            CreateMap<Anexo, AnexoModeloNegocio>()
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental));

            CreateMap<AnexoModeloNegocio, Anexo>()
                .ForMember(dest => dest.TipoDocumental, opt => opt.Ignore())
                .ForMember(dest => dest.IdTipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental != null? src.TipoDocumental.Id : (int?)null))
                .ForMember(dest => dest.Processo, opt => opt.Ignore())
                .ForMember(dest => dest.Despacho, opt => opt.Ignore());

            #endregion

            #region Mapeamento de atividade
            CreateMap<Atividade, AtividadeModeloNegocio>()
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(s => s.Funcao));
            #endregion



            #region Mapeamento de Contato
            CreateMap<Contato, ContatoModeloNegocio>();

            CreateMap<ContatoModeloNegocio, Contato>()
                .ForMember(dest => dest.TipoContato, opt => opt.Ignore())
                .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(src => src.TipoContato.Id));
            #endregion

            #region Mapeamento de Despacho
            CreateMap<DespachoModeloNegocio, Despacho>()
                .ForMember(dest => dest.Processo, opt => opt.Ignore())
                .ForMember(dest => dest.GuidOrganizacaoDestino, opt => opt.MapFrom(src => new Guid(src.GuidOrganizacaoDestino)))
                .ForMember(dest => dest.GuidUnidadeDestino, opt => opt.MapFrom(src => new Guid(src.GuidUnidadeDestino)));


            CreateMap<Despacho, DespachoModeloNegocio>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));
            #endregion


            #region Mapeamento de Destinação

            CreateMap<DestinacaoFinal, DestinacaoFinalModeloNegocio>();

            #endregion

            #region Mapeamento de email
            CreateMap<Email, EmailModeloNegocio>();
            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModeloNegocio, Email>();
                ;
            #endregion

            #region Mapeamento de Interessados (Pessoa Física e Jurídica)

            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisica>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridica>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            #endregion

            #region Mapeamento de função
            CreateMap<Funcao, FuncaoModeloNegocio>()
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.MapFrom(s => s.PlanoClassificacao))
                .ForMember(dest => dest.FuncaoPai, opt => opt.MapFrom(s => s.FuncaoPai != null ? Mapper.Map<Funcao, FuncaoModeloNegocio>(s.FuncaoPai) : null))
                .MaxDepth(1);
            #endregion

            #region Mapeamento de interessado pessoa física
            CreateMap<InteressadoPessoaFisica, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<Contato>, List<ContatoModeloNegocio>>(s.Contatos.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<Email>, List<EmailModeloNegocio>>(s.Emails.ToList()) : null));
            #endregion

            #region Mapeamento de interessado pessoa jurídica
            CreateMap<InteressadoPessoaJuridica, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<Contato>, List<ContatoModeloNegocio>>(s.Contatos.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<Email>, List<EmailModeloNegocio>>(s.Emails.ToList()) : null));
            #endregion


            #region Mapeamento de Municipio do Processo
            CreateMap<MunicipioProcessoModeloNegocio, MunicipioProcesso>()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf.ToUpper()));

            CreateMap<MunicipioProcesso, MunicipioProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de organização processo
            CreateMap<OrganizacaoProcesso, OrganizacaoProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { IdOrganizacao = src.IdOrganizacaoProcesso}));
            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoModeloNegocio, Processo>()
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.Atividade, opt => opt.Ignore())
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica))
                .ForMember(dest => dest.MunicipiosProcesso, opt => opt.MapFrom(src => src.MunicipiosProcesso))
                .ForMember(dest => dest.SinalizacoesProcesso, opt => opt.MapFrom(src => src.Sinalizacoes))
                .ForMember(dest => dest.GuidOrganizacaoAutuadora, opt => opt.MapFrom(src => new Guid (src.GuidOrganizacaoAutuadora)))
                .ForMember(dest => dest.GuidUnidadeAutuadora, opt => opt.MapFrom(src => new Guid (src.GuidUnidadeAutuadora)))
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.Ignore());


            CreateMap<Processo, ProcessoModeloNegocio>()
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(s => s.SinalizacoesProcesso != null ? Mapper.Map<List<SinalizacaoProcesso>, List<SinalizacaoModeloNegocio>>(s.SinalizacoesProcesso.ToList()) : null))
                .ForMember(dest => dest.GuidOrganizacaoAutuadora, opt => opt.MapFrom(src => src.GuidOrganizacaoAutuadora.ToString("D")))
                .ForMember(dest => dest.GuidUnidadeAutuadora, opt => opt.MapFrom(src => src.GuidUnidadeAutuadora.ToString("D")))
                .MaxDepth(1);
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

            #region Mapeamento de Sinalização
            CreateMap<Sinalizacao, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { IdOrganizacao = src.IdOrganizacaoProcesso }))
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem == null ? null : src.Imagem));

            
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoProcesso>()
                .ForMember(dest => dest.IdSinalizacao, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<SinalizacaoProcesso, SinalizacaoModeloNegocio>()
                .ConvertUsing(s => s.Sinalizacao != null ? Mapper.Map<Sinalizacao, SinalizacaoModeloNegocio>(s.Sinalizacao) : null);

            #endregion

        }

    }
}


