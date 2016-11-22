﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class MunicipioProcessoModelo
    {
        [Required]
        public string Uf { get; set; }
        [Required]
        public string Nome { get; set; }

    }
}
