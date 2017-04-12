using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public class SinalizacaoRascunhoProcesso
    {
        public int Id { get; set; }
        public int IdSinalizacao { get; set; }
        public int IdRascunhoProcesso { get; set; }

        public virtual RascunhoProcesso RascunhoProcesso { get; set; }
        public virtual Sinalizacao Sinalizacao { get; set; }

    }
}
