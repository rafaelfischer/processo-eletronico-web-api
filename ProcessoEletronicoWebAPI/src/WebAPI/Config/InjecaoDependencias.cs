using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Apresentacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;

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
                services.AddTransient(dep.Key, dep.Value);
            }

            //Demais dependências da camada de negócio (que não possuem interfaces)
            services.AddTransient(typeof(UsuarioValidacao));
            services.AddTransient(typeof(RascunhoProcessoValidacao));
            services.AddTransient(typeof(InteressadoPessoaFisicaValidacao));
            services.AddTransient(typeof(InteressadoPessoaJuridicaValidacao));
            services.AddTransient(typeof(ContatoValidacao));
            services.AddTransient(typeof(EmailValidacao));
            services.AddTransient(typeof(OrganogramaValidacao));
        }
    }
}
