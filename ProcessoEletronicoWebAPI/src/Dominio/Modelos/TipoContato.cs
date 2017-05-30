using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class TipoContato
    {
        public TipoContato()
        {
            Contatos = new HashSet<Contato>();
            ContatosRascunho = new HashSet<ContatoRascunho>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte QuantidadeDigitos { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<ContatoRascunho> ContatosRascunho { get; set; }
    }
}
