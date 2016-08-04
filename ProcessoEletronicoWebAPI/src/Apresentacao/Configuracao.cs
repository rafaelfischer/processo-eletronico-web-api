using ProcessoEletronicoService.Negocio;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao
{
    public static class Configuracao
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Negocio.Configuracao.ObterDependencias();
            dependencias.Add(typeof(IAutuacao), typeof(Autuacao));

            return dependencias;
        }
    }
}
