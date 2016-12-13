using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Config;
using System.Collections.Generic;
using System;

namespace ProcessoEletronicoService.Apresentacao.Config
{
    public static class ConfiguracaoAutoMapper
    {
        public static ApresentacaoProfile GetApresentacaoProfile()
        {
            return new ApresentacaoProfile();
        }

        public static void ExecutaMapeamento()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile<ApresentacaoProfile>();
                cfg.AddProfile<NegocioProfile>();
            });
        }

    }
    public class ApresentacaoProfile : Profile
    {

        public ApresentacaoProfile()
        {
            #region Mapeamento de atividade
            CreateMap<AtividadeModeloNegocio, AtividadeModelo>()
                .ForMember(dest => dest.IdFuncao, opt => opt.MapFrom(src => src.Funcao.Id));
            CreateMap<AtividadeModeloNegocio, AtividadeProcessoGetModelo>();

            CreateMap<AtividadeModelo, AtividadeModeloNegocio>();
            #endregion

            #region Mapeamento de Anexos
            CreateMap<AnexoModelo, AnexoModeloNegocio>()
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore())
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.IdTipoDocumental.HasValue ? new TipoDocumentalModeloNegocio { Id = src.IdTipoDocumental.Value } : null));

            CreateMap<AnexoModeloNegocio, AnexoModeloGet>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.Conteudo != null ? Convert.ToBase64String(src.Conteudo) : null))
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental));

            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModelo, EmailModeloNegocio>();

            CreateMap<EmailModeloNegocio, EmailModelo>();
            #endregion

            #region Mapeamento de Contato
            CreateMap<ContatoModeloNegocio, ContatoProcessoGetModelo>();
            CreateMap<ContatoModelo, ContatoModeloNegocio>()
                .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src => src.IdTipoContato));
            #endregion

            #region Mapeamento de Despacho

            CreateMap<DespachoProcessoModeloPost, DespachoModeloNegocio>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<DespachoModeloNegocio, DespachoProcessoModeloGet>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<DespachoModeloNegocio, DespachoProcessoModeloCompleto>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.Processo, opt => opt.MapFrom(src => src.Processo));
            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));

            CreateMap<FuncaoModeloNegocio, FuncaoProcessoGetModelo>();
            #endregion

            #region Mapeamento de Interessados Pessoa Física
            CreateMap<InteressadoPessoaFisicaModelo, InteressadoPessoaFisicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.NomeMunicipio, opt => opt.MapFrom(src => src.NomeMunicipio));

            CreateMap<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisicaProcessoGetModelo>();
            #endregion

            #region Mapeamento de Interessados  Pessoa Jurídica
            CreateMap<InteressadoPessoaJuridicaModelo, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails))
                .ForMember(dest => dest.NomeMunicipio, opt => opt.MapFrom(src => src.NomeMunicipio));

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaProcessoGetModelo>();
            #endregion

            #region Mapeamento de Municipio
            CreateMap<MunicipioProcessoModelo, MunicipioProcessoModeloNegocio>();

            CreateMap<MunicipioProcessoModeloNegocio, MunicipioProcessoModelo>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id));

            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoProcessoGetModelo>();
            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoModeloPost, ProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => new AtividadeModeloNegocio { Id = src.IdAtividade }))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaFisicaModelo>, List<InteressadoPessoaFisicaModeloNegocio>>(src.InteressadosPessoaFisica)))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaJuridicaModelo>, List<InteressadoPessoaJuridicaModeloNegocio>>(src.InteressadosPessoaJuridica)))
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(src => src.IdSinalizacoes))
                .ForMember(dest => dest.Anexos , opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosProcesso, opt => opt.MapFrom(src => src.Municipios));
            CreateMap<ProcessoModeloNegocio, ProcessoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.DataUltimoTramite, opt => opt.MapFrom(src => src.DataUltimoTramite.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id));

            CreateMap<ProcessoModeloNegocio, ProcessoCompletoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.DataUltimoTramite, opt => opt.MapFrom(src => src.DataUltimoTramite.ToString("dd/MM/yyyy HH:mm:ss")))
                //.ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));
            #endregion

            #region Mapeamento de Tipo de Contato

            CreateMap<int, TipoContatoModeloNegocio>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            CreateMap<TipoContatoModeloNegocio, TipoContatoModelo>();

            CreateMap<TipoContatoModeloNegocio, TipoContatoProcessoGetModelo>();

            #endregion

            #region Mapeamento de Tipo Documental

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModelo>().
                ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id));

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalAnexoModelo>();



            #endregion

            #region Mapeamento de Sinalização
            CreateMap<int, SinalizacaoModeloNegocio>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<SinalizacaoModeloNegocio, SinalizacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<SinalizacaoModeloNegocio, SinalizacaoProcessoGetModelo>();
            #endregion


        }

    }
}




