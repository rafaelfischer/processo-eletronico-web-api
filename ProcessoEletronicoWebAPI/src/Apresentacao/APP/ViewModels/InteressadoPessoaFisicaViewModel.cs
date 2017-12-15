using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class InteressadoPessoaFisicaViewModel
    {
        public int Id { get; set; }
        public int IdRascunho { get; set; }

        [DisplayName("Nome")]        
        [Required(ErrorMessage = "Informe o nome do interessado.")]
        public string Nome { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Informe o CPF do interessado.")]
        public string Cpf { get; set; }

        [DisplayName("Município")]
        [Required(ErrorMessage = "Informe o município do interessado.")]
        public string GuidMunicipio { get; set; }

        [DisplayName("Município")]        
        public string NomeMunicipio { get; set; }

        [DisplayName("UF")]        
        public string UfMunicipio { get; set; }

        public List<ContatoViewModel> Contatos { get; set; }
        public List<EmailViewModel> Emails { get; set; }

        public List<TipoContatoViewModel> TiposContato { get; set; }
        public List<UfViewModel> Ufs { get; set; }
        public IEnumerable<MunicipioViewModel> Municipios { get; set; }
    }
}
