using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class OrganizacaoProcesso
    {
        public OrganizacaoProcesso()
        {
            PlanosClassificacao = new HashSet<PlanoClassificacao>();
            Processos = new HashSet<Processo>();
            Sinalizacoes = new HashSet<Sinalizacao>();
        }

        public int Id { get; set; }
        public int? IdOrganizacao { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public int IdDigitoPoder { get; set; }
        public int IdDigitoEsfera { get; set; }
        public short DigitoOrganizacao { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public virtual ICollection<PlanoClassificacao> PlanosClassificacao { get; set; }
        public virtual ICollection<Processo> Processos { get; set; }
        public virtual ICollection<Sinalizacao> Sinalizacoes { get; set; }
        public virtual DigitoEsfera DigitoEsfera { get; set; }
        public virtual DigitoPoder DigitoPoder { get; set; }
    }
}
