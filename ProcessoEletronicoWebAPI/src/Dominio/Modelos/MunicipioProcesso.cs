using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class MunicipioProcesso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public int IdProcesso { get; set; }

        public virtual Processo IdProcessoNavigation { get; set; }
    }
}
