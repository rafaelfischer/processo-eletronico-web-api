using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Dependencies;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Config
{
    public static class DependencyInjection
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            //provisorio
            ProcessoEletronicoContext.ConnectionString = Environment.GetEnvironmentVariable("ProcessoEletronicoConnectionString");

            Dictionary<Type, Type> dependenciesDictionary = ProjectsDependencies.GetDependenciesDictionary();
            List<Type> dependenciesCollection = ProjectsDependencies.GetDependenciesCollection().ToList();

            foreach (var dependency in dependenciesDictionary)
            {
                services.AddScoped(dependency.Key, dependency.Value);
            }
            
            foreach (var dependency in dependenciesCollection)
            {
                services.AddScoped(dependency);
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserProvider, CurrentUser>();
            services.AddScoped<IClientAccessTokenProvider, AccessTokenProvider>();
        }
    }
}
