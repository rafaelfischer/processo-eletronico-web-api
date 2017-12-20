using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apresentacao.APP.ViewModels
{
    public class InteressadoPessoaJuridicaViewModel
    {
        public int Id { get; set; }
        public int IdRascunho { get; set; }
        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "Informe a razão social do interessado.")]
        public string RazaoSocial { get; set; }

        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "Informe o CNPJ do interessado.")]
        public string Cnpj { get; set; }

        [DisplayName("Sigla")]        
        public string Sigla { get; set; }

        [DisplayName("Unidade")]        
        public string NomeUnidade { get; set; }

        [DisplayName("Sigla da Unidade")]        
        public string SiglaUnidade { get; set; }

        [DisplayName("Município")]
        [MinLength(10, ErrorMessage = "Informe o município do interessado.")]
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
