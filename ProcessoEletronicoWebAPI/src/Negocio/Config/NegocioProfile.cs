using System;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Config
{
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

            CreateMap<AtividadeModeloNegocio, Atividade>()
                .ForMember(dest => dest.Funcao, opt => opt.Ignore())
                .ForMember(dest => dest.IdFuncao, opt => opt.MapFrom(src => src.Funcao.Id));
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
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .MaxDepth(1);
            
            #endregion

            #region Mapeamento de Destinação

            CreateMap<DestinacaoFinal, DestinacaoFinalModeloNegocio>();
            CreateMap<DestinacaoFinalModeloNegocio, DestinacaoFinal>();

            #endregion

            #region Mapeamento de email
            CreateMap<Email, EmailModeloNegocio>();
            #endregion

            #region Mapeamento de Email
            CreateMap<EmailModeloNegocio, Email>();
                ;
            #endregion

            #region Mapeamento de Interesasdo Pessoa Jurídica

            CreateMap<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridica>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => new Guid(src.GuidMunicipio)))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(src => src.Emails));

            CreateMap<InteressadoPessoaJuridica, InteressadoPessoaJuridicaModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.ToString("D")))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(s => s.Contatos != null ? Mapper.Map<List<Contato>, List<ContatoModeloNegocio>>(s.Contatos.ToList()) : null))
                .ForMember(dest => dest.Emails, opt => opt.MapFrom(s => s.Emails != null ? Mapper.Map<List<Email>, List<EmailModeloNegocio>>(s.Emails.ToList()) : null));
            #endregion

            #region Mapeamento de função
            CreateMap<Funcao, FuncaoModeloNegocio>()
                .ForMember(dest => dest.PlanoClassificacao, opt => opt.MapFrom(s => s.PlanoClassificacao))
                .ForMember(dest => dest.FuncaoPai, opt => opt.MapFrom(s => s.FuncaoPai != null ? Mapper.Map<Funcao, FuncaoModeloNegocio>(s.FuncaoPai) : null))
                .MaxDepth(1);

            CreateMap<FuncaoModeloNegocio, Funcao>()
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id))
                .ForMember(dest => dest.IdFuncaoPai, opt => opt.MapFrom(src => src.FuncaoPai != null ? src.FuncaoPai.Id : (int?)null))
                .ForMember(dest => dest.IdPlanoClassificacao, opt => opt.MapFrom(src => src.PlanoClassificacao.Id));

            #endregion
            
            #region Mapeamento de Municipio do Rascunho do Processo
            CreateMap<MunicipioRascunhoProcessoModeloNegocio, MunicipioRascunhoProcesso>()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf.ToUpper()))
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => new Guid(src.GuidMunicipio)));

            CreateMap<MunicipioRascunhoProcesso, MunicipioRascunhoProcessoModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.HasValue ? src.GuidMunicipio.Value.ToString("D") : null));
            #endregion

            #region Mapeamento de Municipio do Processo
            CreateMap<MunicipioProcessoModeloNegocio, MunicipioProcesso>()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf.ToUpper()))
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => new Guid(src.GuidMunicipio)));

            CreateMap<MunicipioProcesso, MunicipioProcessoModeloNegocio>()
                .ForMember(dest => dest.GuidMunicipio, opt => opt.MapFrom(src => src.GuidMunicipio.ToString("D")));
            #endregion

            #region Mapeamento de organização processo
            CreateMap<OrganizacaoProcesso, OrganizacaoProcessoModeloNegocio>();
            #endregion

            #region Mapeamento de plano de classificação
            CreateMap<PlanoClassificacao, PlanoClassificacaoModeloNegocio>()
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.MapFrom(src => new OrganizacaoProcessoModeloNegocio() { Id = src.IdOrganizacaoProcesso }))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => src.GuidOrganizacao.ToString("D")));
                
            CreateMap<PlanoClassificacaoModeloNegocio, PlanoClassificacao>()
                .ForMember(dest => dest.IdOrganizacaoProcesso, opt => opt.MapFrom(src => src.OrganizacaoProcesso.Id))
                .ForMember(dest => dest.OrganizacaoProcesso, opt => opt.Ignore())
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => new Guid(src.GuidOrganizacao)));
            #endregion

            #region Mapeamento de Rascunho de Processo
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
                .ForMember(dest => dest.Sinalizacoes, opt => opt.MapFrom(s => s.SinalizacoesRascunhoProcesso != null ? Mapper.Map<List<SinalizacaoRascunhoProcesso>, List<SinalizacaoModeloNegocio>>(s.SinalizacoesRascunhoProcesso.ToList()) : null))
                .ForMember(dest => dest.InteressadosPessoaFisica, opt => opt.MapFrom(src => src.InteressadosPessoaFisica.ToList()))
                .ForMember(dest => dest.InteressadosPessoaJuridica, opt => opt.MapFrom(src => src.InteressadosPessoaJuridica.ToList()))
                .ForMember(dest => dest.Anexos, opt => opt.MapFrom(src => src.Anexos))
                .ForMember(dest => dest.MunicipiosRascunhoProcesso, opt => opt.MapFrom(src => src.MunicipiosRascunhoProcesso))
                .ForMember(dest => dest.GuidOrganizacao, opt => opt.MapFrom(src => src.GuidOrganizacao.ToString("D")))
                .ForMember(dest => dest.GuidUnidade, opt => opt.MapFrom(src => src.GuidUnidade.ToString("D")))
                .MaxDepth(1);
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
               .ForMember(dest => dest.DestinacaoFinal, opt => opt.MapFrom(src => src.DestinacaoFinal))
               .ForMember(dest => dest.Atividade, opt => opt.MapFrom(src => src.Atividade));


            CreateMap<TipoDocumentalModeloNegocio, TipoDocumental>()
                .ForMember(dest => dest.Atividade, opt => opt.Ignore())
                .ForMember(dest => dest.DestinacaoFinal, opt => opt.Ignore())
                .ForMember(dest => dest.IdAtividade, opt => opt.MapFrom(src => src.Atividade.Id))
                .ForMember(dest => dest.IdDestinacaoFinal, opt => opt.MapFrom(src => src.DestinacaoFinal.Id));

            #endregion
            
            #region Mapeamento de Sinalização
            CreateMap<Sinalizacao, SinalizacaoModeloNegocio>()
                .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem == null ? null : src.Imagem))
                .MaxDepth(1);

            
            CreateMap<SinalizacaoModeloNegocio, SinalizacaoProcesso>()
                .ForMember(dest => dest.IdSinalizacao, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<SinalizacaoProcesso, SinalizacaoModeloNegocio>()
                .ConvertUsing(s => s.Sinalizacao != null ? Mapper.Map<Sinalizacao, SinalizacaoModeloNegocio>(s.Sinalizacao) : null);

            #endregion

        }

    }
}


