using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class AnexoModelo
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public byte[] Conteudo { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
