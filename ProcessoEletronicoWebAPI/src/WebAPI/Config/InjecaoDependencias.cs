using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Apresentacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using Negocio.Comum.Validacao;
using Negocio.Bloqueios;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public static class InjecaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Apresentacao.Configuracao.ConfiguracaoDepedencias.ObterDependencias();

            return dependencias;
        }

        public static void InjetarDependencias (IServiceCollection services)
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias = ObterDependencias();

            foreach (var dep in dependencias)
            {
                services.AddScoped(dep.Key, dep.Value);
            }

            //Demais dependências da camada de negócio (que não possuem interfaces)
            services.AddScoped(typeof(UsuarioValidacao));
            services.AddScoped(typeof(AnexoValidacao));
            services.AddScoped(typeof(BloqueioValidation));
            services.AddScoped(typeof(RascunhoProcessoValidacao));
            services.AddScoped(typeof(InteressadoPessoaFisicaValidacao));
            services.AddScoped(typeof(InteressadoPessoaJuridicaValidacao));
            services.AddScoped(typeof(ContatoValidacao));
            services.AddScoped(typeof(EmailValidacao));
            services.AddScoped(typeof(MunicipioValidacao));
            services.AddScoped(typeof(Negocio.Validacao.ProcessoValidacao));
            services.AddScoped(typeof(SinalizacaoValidacao));
            services.AddScoped(typeof(Negocio.Sinalizacoes.Validacao.SinalizacoesValidacao));
            services.AddScoped(typeof(OrganogramaValidacao));
            services.AddScoped(typeof(GuidValidacao));
        }
    }
}
