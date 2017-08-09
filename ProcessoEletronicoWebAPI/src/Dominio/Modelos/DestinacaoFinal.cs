using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class DestinacaoFinal
    {
        public DestinacaoFinal()
        {
            TiposDocumentais = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<TipoDocumental> TiposDocumentais { get; set; }
    }
}
