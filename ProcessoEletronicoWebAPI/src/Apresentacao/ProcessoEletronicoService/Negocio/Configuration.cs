using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio
{
    public class Configuration
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias.Add(typeof(IAutuacao), typeof(Autuacao));

            return dependencias;
        }
    }
}