﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class SinalizacaoProcesso
    {
        public int Id { get; set; }
        public int IdSinalizacao { get; set; }
        public int IdProcesso { get; set; }

        public virtual Processo Processo { get; set; }
        public virtual Sinalizacao Sinalizacao { get; set; }
    }
}
