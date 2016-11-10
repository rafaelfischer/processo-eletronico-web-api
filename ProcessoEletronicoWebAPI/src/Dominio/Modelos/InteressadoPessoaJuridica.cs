using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaJuridica
    {
        public InteressadoPessoaJuridica()
        {
            Contato = new HashSet<Contato>();
            Email = new HashSet<Email>();
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

        public virtual ICollection<Contato> Contato { get; set; }
        public virtual ICollection<Email> Email { get; set; }
        public virtual Processo IdProcessoNavigation { get; set; }
    }
}
