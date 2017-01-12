using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class TipoDocumentalModeloNegocio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte? PrazoGuardaAnosCorrente { get; set; }
        public string PrazoGuardaSubjetivoCorrente { get; set; }
        public byte? PrazoGuardaAnosIntermediaria { get; set; }
        public string PrazoGuardaSubjetivoIntermediaria { get; set; }
        public string Observacao { get; set; }
        public DestinacaoFinalModeloNegocio DestinacaoFinal { get; set; }
        public AtividadeModeloNegocio Atividade { get; set; }
        public bool Obrigatorio { get; set; }
    }
}
