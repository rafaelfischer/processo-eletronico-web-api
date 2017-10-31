using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class OrganizacaoViewModel
    {     
        public string guid { get; set; }
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string sigla { get; set; }
        public List<ContatoViewModel> contatos { get; set; }
        public List<EmailViewModel> emails { get; set; }
        public EnderecoViewModel endereco { get; set; }
        //public Esfera esfera { get; set; }
        //public Poder poder { get; set; }
        //public Organizacaopai organizacaoPai { get; set; }
        //public List<Site> sites { get; set; }
        //public Tipoorganizacao tipoOrganizacao { get; set; }
        public string NomeSigla { get
            {
                return this.sigla + " - "+ this.razaoSocial;
            }
        }
    }   

    public class Esfera
    {
        public string descricao { get; set; }
    }

    public class Poder
    {
        public string descricao { get; set; }
    }

    public class Organizacaopai
    {
        public string guid { get; set; }
        public string razaoSocial { get; set; }
        public string sigla { get; set; }
    }

    public class Tipoorganizacao
    {
        public string descricao { get; set; }
    }

    public class Contato
    {
        public string telefone { get; set; }
        public TipoContatoViewModel tipoContato { get; set; }
    }   

    public class Email
    {
        public string endereco { get; set; }
    }

    public class Site
    {
        public string url { get; set; }
    }

}
