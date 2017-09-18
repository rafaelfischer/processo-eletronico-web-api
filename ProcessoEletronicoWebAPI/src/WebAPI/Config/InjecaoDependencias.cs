using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Dependencies;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.WebAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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
