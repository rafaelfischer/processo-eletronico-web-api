using ProcessoEletronicoService.Negocio.Restrito;
using ProcessoEletronicoService.Negocio.Restrito.Base;
using ProcessoEletronicoService.Negocio.Publico;
using ProcessoEletronicoService.Negocio.Publico.Base;
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
            dependencias.Add(typeof(IConsultaProcesso), typeof(ConsultaProcesso));
            dependencias.Add(typeof(ITipoDocumentalNegocio), typeof(TipoDocumentalNegocio));
            dependencias.Add(typeof(ISinalizacaoNegocio), typeof(SinalizacaoNegocio));

            return dependencias;
        }
    }
}
