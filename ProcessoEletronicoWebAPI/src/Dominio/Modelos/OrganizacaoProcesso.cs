using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class OrganizacaoProcesso
    {
        public OrganizacaoProcesso()
        {
            PlanoClassificacao = new HashSet<PlanoClassificacao>();
            Processo = new HashSet<Processo>();
            Sinalizacao = new HashSet<Sinalizacao>();
        }

        public int Id { get; set; }
        public int IdOrganizacao { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdDigitoPoder { get; set; }
        public int IdDigitoEsfera { get; set; }
        public short NumeroOrganiacao { get; set; }

        public virtual ICollection<PlanoClassificacao> PlanoClassificacao { get; set; }
        public virtual ICollection<Processo> Processo { get; set; }
        public virtual ICollection<Sinalizacao> Sinalizacao { get; set; }
        public virtual DigitoEsfera DigitoEsfera { get; set; }
        public virtual DigitoPoder DigitoPoder { get; set; }
    }
}
