using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class TipoContato
    {
        public TipoContato()
        {
            Contato = new HashSet<Contato>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte QuantidadeDigitos { get; set; }

        public virtual ICollection<Contato> Contato { get; set; }
    }
}
