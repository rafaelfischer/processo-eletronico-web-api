using Negocio.Bloqueios;
using Negocio.Bloqueios.Base;
using Negocio.Comum.Validacao;
using Negocio.Notificacoes;
using Negocio.Notificacoes.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Negocio;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
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

            #region Negocio

            //Classes de Negócio envolvidas nas operações CRUD
            dependencies.Add(typeof(Negocio.Base.IAnexoNegocio), typeof(Negocio.AnexoNegocio));
            dependencies.Add(typeof(Negocio.Rascunho.Processo.Base.IAnexoNegocio), typeof(Negocio.Rascunho.Processo.AnexoNegocio));
            dependencies.Add(typeof(IBloqueioCore), typeof(BloqueioCore));
            dependencies.Add(typeof(IAtividadeNegocio), typeof(AtividadeNegocio));
            dependencies.Add(typeof(IContatoInteressadoPessoaFisicaNegocio), typeof(ContatoInteressadoPessoaFisicaNegocio));
            dependencies.Add(typeof(IContatoInteressadoPessoaJuridicaNegocio), typeof(ContatoInteressadoPessoaJuridicaNegocio));
            dependencies.Add(typeof(IEmailInteressadoPessoaFisicaNegocio), typeof(EmailInteressadoPessoaFisicaNegocio));
            dependencies.Add(typeof(IEmailInteressadoPessoaJuridicaNegocio), typeof(EmailInteressadoPessoaJuridicaNegocio));
            dependencies.Add(typeof(IDespachoNegocio), typeof(DespachoNegocio));
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

            #endregion

            #region Apresentacao
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
            dependenciesCollection.Add(typeof(ProcessoEletronicoService.Negocio.Validacao.ProcessoValidacao));
            dependenciesCollection.Add(typeof(SinalizacaoValidacao));
            dependenciesCollection.Add(typeof(ProcessoEletronicoService.Negocio.Sinalizacoes.Validacao.SinalizacoesValidacao));
            dependenciesCollection.Add(typeof(OrganogramaValidacao));
            dependenciesCollection.Add(typeof(GuidValidacao));

            return dependenciesCollection;
        }
    }
}
