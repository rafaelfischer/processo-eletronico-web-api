using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class TipoDocumentalViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte? PrazoGuardaAnosCorrente { get; set; }
        public string PrazoGuardaSubjetivoCorrente { get; set; }
        public byte? PrazoGuardaAnosIntermediaria { get; set; }
        public string PrazoGuardaSubjetivoIntermediaria { get; set; }
        public string Observacao { get; set; }
        
        public string Text { get { return this.Codigo + " - " + this.Descricao; } }
    }
}
