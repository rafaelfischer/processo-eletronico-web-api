using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaFisicaRascunho
    {
        public InteressadoPessoaFisicaRascunho()
        {
            ContatosRascunho = new HashSet<ContatoRascunho>();
            EmailsRascunho = new HashSet<EmailRascunho>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int? IdRascunhoProcesso { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public Guid? GuidMunicipio { get; set; }

        public virtual ICollection<ContatoRascunho> ContatosRascunho { get; set; }
        public virtual ICollection<EmailRascunho> EmailsRascunho { get; set; }
        public virtual RascunhoProcesso RascunhoProcesso { get; set; }
    }
}
