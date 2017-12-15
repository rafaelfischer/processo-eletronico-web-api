using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class OrganizacaoViewModel
    {     
        public string Guid { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public List<ContatoViewModel> Contatos { get; set; }
        public List<EmailViewModel> Emails { get; set; }
        public EnderecoViewModel Endereco { get; set; }        
        public string NomeSigla { get
            {
                return this.Sigla + " - "+ this.RazaoSocial;
            }
        }
    }   

    public class Esfera
    {
        public string Descricao { get; set; }
    }

    public class Poder
    {
        public string Descricao { get; set; }
    }

    public class Organizacaopai
    {
        public string Guid { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
    }

    public class Tipoorganizacao
    {
        public string Descricao { get; set; }
    }

    public class Contato
    {
        public string Telefone { get; set; }
        public TipoContatoViewModel TipoContato { get; set; }
    }   

    public class Email
    {
        public string Endereco { get; set; }
    }

    public class Site
    {
        public string Url { get; set; }
    }

}
