using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios;

namespace ProcessoEletronicoService.Negocio.Config
{
    public static class ConfiguracaoDependencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias.Add(typeof(IProcessoEletronicoRepositorios), typeof(ProcessoEletronicoRepositorios));
            
            return dependencias;
        }
    }
}