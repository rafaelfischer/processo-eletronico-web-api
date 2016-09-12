using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Apresentacao.Publico;
using ProcessoEletronicoService.Apresentacao.Config;


namespace ProcessoEletronicoService.WebAPI.Publico.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            //Dependecias de Apresentacao
            dependencias = ConfiguracaoDepedencias.ObterDependencias();
            dependencias.Add(typeof(IConsultaProcessoWorkService), typeof(ConsultaProcessoWorkService));

            return dependencias;
        }

        public static void InjetarDependencias(IServiceCollection services)
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();
            dependencias = ObterDependencias();

            foreach (var dep in dependencias)
            {
                services.AddTransient(dep.Key, dep.Value);
            }

        }
    }
}
