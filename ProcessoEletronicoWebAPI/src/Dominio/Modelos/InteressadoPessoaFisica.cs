using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaFisica
    {
        public InteressadoPessoaFisica()
        {
            Contatos = new HashSet<Contato>();
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int IdProcesso { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public Guid GuidMunicipio { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
