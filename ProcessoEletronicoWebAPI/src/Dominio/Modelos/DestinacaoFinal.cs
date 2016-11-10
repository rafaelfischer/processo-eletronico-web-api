using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class DestinacaoFinal
    {
        public DestinacaoFinal()
        {
            TipoDocumental = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<TipoDocumental> TipoDocumental { get; set; }
    }
}
