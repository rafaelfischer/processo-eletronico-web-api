using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class InteressadoPessoaJuridicaRascunho
    {
        public InteressadoPessoaJuridicaRascunho()
        {
            ContatosRascunho = new HashSet<ContatoRascunho>();
            EmailsRascunho = new HashSet<EmailRascunho>();
        }

        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public int? IdRascunhoProcesso { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public Guid? GuidMunicipio { get; set; }

        public virtual ICollection<ContatoRascunho> ContatosRascunho { get; set; }
        public virtual ICollection<EmailRascunho> EmailsRascunho { get; set; }
        public virtual RascunhoProcesso RascunhoProcesso { get; set; }
    }
}
