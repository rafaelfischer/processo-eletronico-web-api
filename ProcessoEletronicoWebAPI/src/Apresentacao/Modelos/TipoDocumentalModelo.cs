using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class TipoDocumentalModeloPost
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdAtividade { get; set; }
        public byte? PrazoGuardaAnosCorrente { get; set; }
        public byte? PrazoGuardaAnosIntermediaria { get; set; }
        public string PrazoGuardaSubjetivoCorrente { get; set; }
        public string PrazoGuardaSubjetivoIntermediaria { get; set; }
        public int IdDestinacaoFinal { get; set; }
        public bool Obrigatorio { get; set; }
        public string Observacao { get; set; }

    }

    public class TipoDocumentalModeloGet : TipoDocumentalModeloPost
    {
        public int Id { get; set; }
        public AtividadeProcessoGetModelo Atividade {get; set;}
        public DestinacaoFinalModeloGet DestinacaoFinal {get; set; }

    }

    public class TipoDocumentalAnexoModelo
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
