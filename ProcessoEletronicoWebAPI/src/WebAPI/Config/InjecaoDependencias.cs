using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Apresentacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using Negocio.Comum.Validacao;
using Negocio.Bloqueios;
using Dependency;
using System.Linq;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPI.Common;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public static class InjecaoDependencias
    {
        public static void InjetarDependencias(IServiceCollection services)
        {
            Dictionary<Type, Type> dependenciesDictionary = new Dictionary<Type, Type>();
            dependenciesDictionary = ProjectsDependencies.GetDependenciesDictionary();

            foreach (var dep in dependenciesDictionary)
            {
                services.AddScoped(dep.Key, dep.Value);
            }

            //Dependencias que não possuem interface (será refatorado)
            List<Type> dependenciesList = new List<Type>();
            dependenciesList = ProjectsDependencies.GetDependenciesCollection().ToList();

            foreach (var dep in dependenciesList)
            {
                services.AddScoped(dep);
            }

            services.AddScoped<ICurrentUserProvider, CurrentUser>();

        }
    }
}
