using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class InteressadoPessoaFisicaModelo
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        [Required]
        public string Uf { get; set; }
        [Required]
        public string Municipio { get; set; }
    }
     

}
