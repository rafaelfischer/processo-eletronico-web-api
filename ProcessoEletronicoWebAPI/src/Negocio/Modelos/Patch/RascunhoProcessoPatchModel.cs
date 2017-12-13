using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Modelos.Patch
{
    public class RascunhoProcessoPatchModel
    {
        public AtividadeModeloNegocio Atividade { get; set; }
        public string Resumo { get; set; }
        public string GuidUnidade { get; set; }
    }
}
