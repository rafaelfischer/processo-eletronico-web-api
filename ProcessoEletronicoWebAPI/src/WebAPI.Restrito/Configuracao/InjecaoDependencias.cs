using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Processo;
using ProcessoEletronicoService.Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Apresentacao.Restrito.Base;
using ProcessoEletronicoService.Apresentacao.Restrito;

namespace ProcessoEletronicoService.WebAPI.Restrito.Configuracao
{
    public static class InjecaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Apresentacao.Configuracao.ObterDependencias();
            dependencias.Add(typeof(IAutuacaoWorkService), typeof(AutuacaoWorkService));
            dependencias.Add(typeof(ITipoDocumentalWorkService), typeof(TipoDocumentalWorkService));

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

        }


    }
}
