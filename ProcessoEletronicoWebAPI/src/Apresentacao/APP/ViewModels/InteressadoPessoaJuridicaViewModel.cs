using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class InteressadoPessoaJuridicaViewModel
    {
        public int Id { get; set; }
        public int IdRascunho { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public List<ContatoViewModel> Contatos { get; set; }
        public List<EmailViewModel> Emails { get; set; }

        public List<TipoContatoViewModel> TiposContato { get; set; }
        public List<UfViewModel> Ufs { get; set; }
        public IEnumerable<MunicipioViewModel> Municipios { get; set; }
    }
}
