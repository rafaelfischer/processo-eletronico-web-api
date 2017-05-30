using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio;
using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Negocio.Rascunho.Processo;
using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;

namespace ProcessoEletronicoService.Apresentacao.Configuracao
{
    public static class ConfiguracaoDepedencias
    {
        public static Dictionary<Type, Type> ObterDependencias()
        {
            Dictionary<Type, Type> dependencias = new Dictionary<Type, Type>();

            dependencias = Negocio.Config.ConfiguracaoDependencias.ObterDependencias();
            dependencias.Add(typeof(IAnexoNegocio), typeof(AnexoNegocio));
            dependencias.Add(typeof(IAtividadeNegocio), typeof(AtividadeNegocio));
            dependencias.Add(typeof(IContatoInteressadoPessoaFisicaNegocio), typeof(ContatoInteressadoPessoaFisicaNegocio));
            dependencias.Add(typeof(IDespachoNegocio), typeof(DespachoNegocio));
            dependencias.Add(typeof(IDestinacaoFinalNegocio), typeof(DestinacaoFinalNegocio));
            dependencias.Add(typeof(IInteressadoPessoaFisicaNegocio), typeof(InteressadoPessoaFisicaNegocio));
            //dependencias.Add(typeof(IInteressadoPessoaJuridicaNegocio), typeof(InteressadoPessoaJuridicaNegocio));
            dependencias.Add(typeof(IFuncaoNegocio), typeof(FuncaoNegocio));
            dependencias.Add(typeof(IRascunhoProcessoNegocio), typeof(RascunhoProcessoNegocio));
            dependencias.Add(typeof(IPlanoClassificacaoNegocio), typeof(PlanoClassificacaoNegocio));
            dependencias.Add(typeof(IProcessoNegocio), typeof(ProcessoNegocio));
            dependencias.Add(typeof(ITipoDocumentalNegocio), typeof(TipoDocumentalNegocio));
            dependencias.Add(typeof(ITipoContatoNegocio), typeof(TipoContatoNegocio));
            dependencias.Add(typeof(ISinalizacaoNegocio), typeof(SinalizacaoNegocio));

            return dependencias;
        }
    }
}
