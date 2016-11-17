using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public partial class AtividadeModeloNegocio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        //public virtual List<Processo> Processo { get; set; }
        //public virtual List<TipoDocumental> TipoDocumental { get; set; }
        public FuncaoModeloNegocio Funcao { get; set; }
    }
}
