using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Apresentacao.Base;
using Microsoft.Extensions.DependencyInjection;
using ProcessoEletronicoService.Apresentacao;

namespace ProcessoEletronicoService.WebAPI.Config
{
    public static class InjecaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Apresentacao.Configuracao.ConfiguracaoDepedencias.ObterDependencias();
            dependencias.Add(typeof(IAnexoWorkService), typeof(AnexoWorkService));
            dependencias.Add(typeof(IAtividadeWorkService), typeof(AtividadeWorkService));
            dependencias.Add(typeof(IDespachoWorkService), typeof(DespachoWorkService));
            dependencias.Add(typeof(IFuncaoWorkService), typeof(FuncaoWorkService));
            dependencias.Add(typeof(IPlanoClassificacaoWorkService), typeof(PlanoClassificacaoWorkService));
            dependencias.Add(typeof(IProcessoWorkService), typeof(ProcessoWorkService));
            dependencias.Add(typeof(ITipoDocumentalWorkService), typeof(TipoDocumentalWorkService));
            dependencias.Add(typeof(ITipoContatoWorkService), typeof(TipoContatoWorkService));
            dependencias.Add(typeof(ISinalizacaoWorkService), typeof(SinalizacaoWorkService));

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
