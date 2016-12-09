using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaJuridica
    {
        public InteressadoPessoaJuridica()
        {
            Contatos = new HashSet<Contato>();
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public int IdProcesso { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public Guid GuidMunicipio { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
