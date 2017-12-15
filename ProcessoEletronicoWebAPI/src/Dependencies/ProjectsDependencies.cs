using Apresentacao.APP.Services;
using Apresentacao.APP.Services.Base;
using Apresentacao.APP.WorkServices;
using Apresentacao.WebAPI;
using Apresentacao.WebAPI.Base;
using Infraestrutura.Integrations;
using Infraestrutura.Integrations.Organograma;
using Negocio.Bloqueios;
using Negocio.Bloqueios.Base;
using Negocio.Comum.Validacao;
using Negocio.Notificacoes;
using Negocio.Notificacoes.Base;
using Negocio.RascunhosDespacho;
using Negocio.RascunhosDespacho.Base;
using Negocio.RascunhosDespacho.Validations;
using Negocio.RascunhosDespacho.Validations.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Negocio;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using Prodest.ProcessoEletronico.Integration.Common.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dependencies
{
    public static class ProjectsDependencies
    {
        public static Dictionary<Type, Type> GetDependenciesDictionary()
        {
            Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

            #region Infraestrutura
            dependencies.Add(typeof(IProcessoEletronicoRepositorios), typeof(ProcessoEletronicoRepositorios));
            #endregion

            #region Integration
            dependencies.Add(typeof(IOrganizacaoService), typeof(OrganizacaoService));
            dependencies.Add(typeof(IUnidadeService), typeof(UnidadeService));
            dependencies.Add(typeof(IMunicipioService), typeof(MunicipioService));
            dependencies.Add(typeof(IApiHandler), typeof(ApiHandler));
            #endregion

            #region Negocio

            //Classes de Negócio envolvidas nas operações CRUD
            dependencies.Add(typeof(Negocio.Base.IAnexoNegocio), typeof(Negocio.AnexoNegocio));
            dependencies.Add(typeof(Negocio.Rascunho.Processo.Base.IAnexoNegocio), typeof(Negocio.Rascunho.Processo.AnexoNegocio));
            dependencies.Add(typeof(IAnexoRascunhoDespachoCore), typeof(AnexoRascunhoDespachoCore));
            dependencies.Add(typeof(IBloqueioCore), typeof(BloqueioCore));
            dependencies.Add(typeof(IAtividadeNegocio), typeof(AtividadeNegocio));
            dependencies.Add(typeof(IContatoInteressadoPessoaFisicaNegocio), typeof(ContatoInteressadoPessoaFisicaNegocio));
            dependencies.Add(typeof(IContatoInteressadoPessoaJuridicaNegocio), typeof(ContatoInteressadoPessoaJuridicaNegocio));
            dependencies.Add(typeof(IEmailInteressadoPessoaFisicaNegocio), typeof(EmailInteressadoPessoaFisicaNegocio));
            dependencies.Add(typeof(IEmailInteressadoPessoaJuridicaNegocio), typeof(EmailInteressadoPessoaJuridicaNegocio));
            dependencies.Add(typeof(IDespachoNegocio), typeof(DespachoNegocio));
            dependencies.Add(typeof(IRascunhoDespachoCore), typeof(RascunhoDespachoCore));
            dependencies.Add(typeof(IDestinacaoFinalNegocio), typeof(DestinacaoFinalNegocio));
            dependencies.Add(typeof(IInteressadoPessoaFisicaNegocio), typeof(InteressadoPessoaFisicaNegocio));
            dependencies.Add(typeof(IInteressadoPessoaJuridicaNegocio), typeof(InteressadoPessoaJuridicaNegocio));
            dependencies.Add(typeof(IMunicipioNegocio), typeof(MunicipioNegocio));
            dependencies.Add(typeof(INotificacoesService), typeof(NotificacoesService));
            dependencies.Add(typeof(IFuncaoNegocio), typeof(FuncaoNegocio));
            dependencies.Add(typeof(IRascunhoProcessoNegocio), typeof(RascunhoProcessoNegocio));
            dependencies.Add(typeof(IPlanoClassificacaoNegocio), typeof(PlanoClassificacaoNegocio));
            dependencies.Add(typeof(IProcessoNegocio), typeof(ProcessoNegocio));
            dependencies.Add(typeof(ITipoDocumentalNegocio), typeof(TipoDocumentalNegocio));
            dependencies.Add(typeof(ITipoContatoNegocio), typeof(TipoContatoNegocio));
            dependencies.Add(typeof(Negocio.Base.ISinalizacaoNegocio), typeof(Negocio.SinalizacaoNegocio));
            dependencies.Add(typeof(Negocio.Rascunho.Processo.Base.ISinalizacaoNegocio), typeof(Negocio.Rascunho.Processo.SinalizacaoNegocio));
            dependencies.Add(typeof(Negocio.Sinalizacoes.Base.ISinalizacaoNegocio), typeof(Negocio.Sinalizacoes.SinalizacaoNegocio));

            //Validations
            dependencies.Add(typeof(IRascunhoDespachoValidation), typeof(RascunhoDespachoValidation));
            dependencies.Add(typeof(IAnexoRascunhoDespachoValidation), typeof(AnexoRascunhoDespachoValidation));

            #endregion

            #region Apresentacao

            //WebAPP
            dependencies.Add(typeof(IProcessoService), typeof(ProcessoService));
            dependencies.Add(typeof(IProcessoAnexoService), typeof(ProcessoAnexoService));
            dependencies.Add(typeof(IDespachoService), typeof(DespachoService));
            dependencies.Add(typeof(IRascunhoProcessoService), typeof(RascunhoProcessoService));
            dependencies.Add(typeof(IAutuacaoService), typeof(AutuacaoService));
            dependencies.Add(typeof(IOrganogramaAppService), typeof(OrganogramaAppService));
            dependencies.Add(typeof(IRascunhoProcessoAbrangenciaService), typeof(RascunhoProcessoAbrangenciaService));
            dependencies.Add(typeof(IRascunhoProcessoSinalizacaoService), typeof(RascunhoProcessoSinalizacaoService));
            dependencies.Add(typeof(IRascunhoProcessoAnexoService), typeof(RascunhoProcessoAnexoService));
            dependencies.Add(typeof(IRascunhoProcessoInteressadoService), typeof(RascunhoProcessoInteressadoService));
            dependencies.Add(typeof(IRascunhoProcessoContato), typeof(RascunhoProcessoContato));
            dependencies.Add(typeof(IRascunhoProcessoEmail), typeof(RascunhoProcessoEmail));          
            dependencies.Add(typeof(ISinalizacaoService), typeof(SinalizacaoService));
            dependencies.Add(typeof(IRascunhoDespachoAppAnexoService), typeof(RascunhoDespachoAppAnexoService));
            dependencies.Add(typeof(IRascunhoDespachoAppService), typeof(RascunhoDespachoAppService));
            
            //WebAPI
            dependencies.Add(typeof(IRascunhoDespachoService), typeof(RascunhoDespachoService));
            dependencies.Add(typeof(IAnexoRascunhoDespachoService), typeof(AnexoRascunhoDespachoService));

            #endregion



            return dependencies;
        }

        public static ICollection<Type> GetDependenciesCollection()
        {
            ICollection<Type> dependenciesCollection = new List<Type>();

            //Classes de validação (camada de negócio) que não possuem interface (será refatorado)
            dependenciesCollection.Add(typeof(UsuarioValidacao));
            dependenciesCollection.Add(typeof(AnexoValidacao));
            dependenciesCollection.Add(typeof(BloqueioValidation));
            dependenciesCollection.Add(typeof(RascunhoProcessoValidacao));
            dependenciesCollection.Add(typeof(InteressadoPessoaFisicaValidacao));
            dependenciesCollection.Add(typeof(InteressadoPessoaJuridicaValidacao));
            dependenciesCollection.Add(typeof(ContatoValidacao));
            dependenciesCollection.Add(typeof(EmailValidacao));
            dependenciesCollection.Add(typeof(MunicipioValidacao));
            dependenciesCollection.Add(typeof(Negocio.Validacao.ProcessoValidacao));
            dependenciesCollection.Add(typeof(SinalizacaoValidacao));
            dependenciesCollection.Add(typeof(Negocio.Sinalizacoes.Validacao.SinalizacoesValidacao));
            dependenciesCollection.Add(typeof(GuidValidacao));

            return dependenciesCollection;
        }
    }
}
