using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class InteressadoPessoaFisicaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public List<ContatoViewModel> Contatos { get; set; }
        public List<EmailViewModel> Emails { get; set; }

        public List<TipoContatoViewModel> TiposContato { get; set; }
        public List<UfViewModel> Ufs { get; set; }
        public List<MunicipioViewModel> Municipios { get; set; }
    }
}
