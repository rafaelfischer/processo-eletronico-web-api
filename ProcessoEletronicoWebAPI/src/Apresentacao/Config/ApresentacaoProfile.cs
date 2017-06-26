using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;
using System;

namespace ProcessoEletronicoService.Apresentacao.Config
{
    public class ApresentacaoProfile : Profile
    {

        public ApresentacaoProfile()
        {
            #region Mapeamento de Atividade
            CreateMap<AtividadeModeloNegocio, AtividadeModelo>()
                .ForMember(dest => dest.IdFuncao, opt => opt.MapFrom(src => src.Funcao.Id));
            CreateMap<AtividadeModeloNegocio, AtividadeProcessoGetModelo>();

            CreateMap<AtividadeModelo, AtividadeModeloNegocio>();

            CreateMap<AtividadeModeloPost, AtividadeModeloNegocio>()
                .ForMember(dest => dest.Funcao, opt => opt.MapFrom(src => new FuncaoModeloNegocio { Id = src.IdFuncao }));
            #endregion

            #region Mapeamento de Anexos
            CreateMap<AnexoModelo, AnexoModeloNegocio>()
                .ForMember(dest => dest.Conteudo, opt => opt.Ignore())
                .ForMember(dest => dest.ConteudoString, opt => opt.MapFrom(src => src.Conteudo))
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.IdTipoDocumental.HasValue ? new TipoDocumentalModeloNegocio { Id = src.IdTipoDocumental.Value } : null));

            CreateMap<AnexoModeloNegocio, AnexoModeloGet>()
                .ForMember(dest => dest.Conteudo, opt => opt.MapFrom(src => src.Conteudo != null ? Convert.ToBase64String(src.Conteudo) : null))
                .ForMember(dest => dest.TipoDocumental, opt => opt.MapFrom(src => src.TipoDocumental));

            CreateMap<AnexoModeloNegocio, AnexoSimplesModeloGet>()
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

            CreateMap<ContatoModeloNegocio, ContatoModelo>()
                .ForMember(dest => dest.IdTipoContato, opt => opt.MapFrom(src => src.TipoContato.Id));
            #endregion

            #region Mapeamento de Despacho

            CreateMap<DespachoModeloPost, DespachoModeloNegocio>()
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<DespachoModeloNegocio, DespachoModeloGet>()
                .ForMember(dest => dest.DataHoraDespacho, opt => opt.MapFrom(src => src.DataHoraDespacho.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos));

            CreateMap<DespachoModeloNegocio, DespachoSimplesModeloGet>()
                .ForMember(dest => dest.DataHoraDespacho, opt => opt.MapFrom(src => src.DataHoraDespacho.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos != null && src.Anexos.Count > 0 ? src.Anexos : null));

            #endregion

            #region Mapeamento de função
            CreateMap<FuncaoModeloNegocio, FuncaoModelo>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null));

            CreateMap<FuncaoModeloNegocio, FuncaoProcessoGetModelo>();

            CreateMap<FuncaoModeloPost, FuncaoModeloNegocio>()
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.MapFrom(src => new PlanoClassificacaoModeloNegocio { Id = src.IdPlanoClassificacao }))
                .ForMember(dest => dest.FuncaoPai, opt => opt.MapFrom(src => (src.IdFuncaoPai.HasValue && src.IdFuncaoPai.Value > 0) ? new FuncaoModeloNegocio { Id = src.IdFuncaoPai.Value } : null));
            #endregion

            #region Mapeamento de Destinação Final

            CreateMap<DestinacaoFinalModeloNegocio, DestinacaoFinalModeloGet>();
            CreateMap<DestinacaoFinalModeloPost, DestinacaoFinalModeloNegocio>();


            #endregion

            #region Mapeamento de Interessados Pessoa Jurídica
            CreateMap<InteressadoPessoaJuridicaModelo, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));


            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaProcessoGetModelo>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaModelo>()
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos != null && src.Contatos.Count > 0 ? src.Contatos : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails != null && src.Emails.Count > 0 ? src.Emails : null));
            #endregion

            #region Mapeamento de Municipio
            CreateMap<MunicipioProcessoModeloPost, MunicipioProcessoModeloNegocio>();
            CreateMap<MunicipioProcessoModeloNegocio, MunicipioProcessoModeloGet>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoModelo>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id));

            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacaoProcessoGetModelo>();
            CreateMap<PlanoClassificacaoModeloPost, PlanoClassificacaoModeloNegocio>();

            #endregion

            #region Mapeamento de Processo
            CreateMap<ProcessoModeloPost, ProcessoModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => new AtividadeModeloNegocio { Id = src.IdAtividade }))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => Mapper.Map<List<PostInteressadoPessoaFisicaDto>, List<InteressadoPessoaFisicaModeloNegocio>>(src.InteressadosPessoaFisica)))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => Mapper.Map<List<InteressadoPessoaJuridicaModelo>, List<InteressadoPessoaJuridicaModeloNegocio>>(src.InteressadosPessoaJuridica)))
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(src => src.IdSinalizacoes))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosProcesso, opt => opt.MapFrom(src => src.Municipios));
            CreateMap<ProcessoModeloNegocio, ProcessoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.DataUltimoTramite, opt => opt.MapFrom(src => src.DataUltimoTramite.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id));

            CreateMap<ProcessoModeloNegocio, ProcessoCompletoModelo>()
                .ForMember(dest => dest.DataAutuacao, opt => opt.MapFrom(src => src.DataAutuacao.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.DataUltimoTramite, opt => opt.MapFrom(src => src.DataUltimoTramite.ToString("dd/MM/yyyy HH:mm:ss")))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica != null && src.InteressadosPessoaFisica.Count > 0 ? src.InteressadosPessoaFisica : null))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica != null && src.InteressadosPessoaJuridica.Count > 0 ? src.InteressadosPessoaJuridica : null))
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
            CreateMap<TipoDocumentalModeloPost, TipoDocumentalModeloNegocio>()
                .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => new AtividadeModeloNegocio { Id = src.IdAtividade }))
                .ForMember(dest => dest.DestinacaoFinal, opt => opt.MapFrom(src => new DestinacaoFinalModeloNegocio { Id = src.IdDestinacaoFinal }));

            CreateMap<TipoDocumentalModeloNegocio, TipoDocumentalModeloGet>()
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdDestinacaoFinal, opt => opt.MapFrom(src => src.DestinacaoFinal.Id));


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




