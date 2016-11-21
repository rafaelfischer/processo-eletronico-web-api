using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class InteressadoPessoaJuridicaModelo
    {
        [Required]
        public string RazaoSocial { get; set; }
        [Required]
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
        [Required]
        public string Uf { get; set; }
        [Required]
        public string Municipio { get; set; }
    }
     

}
