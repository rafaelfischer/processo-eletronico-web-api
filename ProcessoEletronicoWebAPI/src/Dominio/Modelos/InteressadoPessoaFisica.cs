using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaFisica
    {
        public InteressadoPessoaFisica()
        {
            Contato = new HashSet<Contato>();
            Email = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int IdProcesso { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }

        public virtual ICollection<Contato> Contato { get; set; }
        public virtual ICollection<Email> Email { get; set; }
        public virtual Processo IdProcessoNavigation { get; set; }
    }
}
