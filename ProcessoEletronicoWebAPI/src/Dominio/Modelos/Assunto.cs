using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Assunto
    {
        public Assunto()
        {
            Processo = new HashSet<Processo>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Processo> Processo { get; set; }
    }
}
