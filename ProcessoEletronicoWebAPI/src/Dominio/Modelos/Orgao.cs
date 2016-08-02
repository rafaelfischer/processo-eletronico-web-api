using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Orgao
    {
        public Orgao()
        {
            Processo = new HashSet<Processo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<Processo> Processo { get; set; }
    }
}
