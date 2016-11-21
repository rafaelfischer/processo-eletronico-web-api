using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class ContatoModelo
    {
        [Required]
        public string Telefone { get; set; }
        [Required]
        public int IdTipoContato { get; set; }
    }
}
