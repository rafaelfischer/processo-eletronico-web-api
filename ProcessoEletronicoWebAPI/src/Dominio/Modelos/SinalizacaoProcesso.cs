using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class SinalizacaoProcesso
    {
        public int Id { get; set; }
        public int IdSinalizacao { get; set; }
        public int IdProcesso { get; set; }

        public virtual Processo IdProcessoNavigation { get; set; }
        public virtual Sinalizacao IdSinalizacaoNavigation { get; set; }
    }
}
