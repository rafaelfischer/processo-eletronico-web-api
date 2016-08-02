using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Processo
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public byte Digito { get; set; }
        public int? AssuntoId { get; set; }
        public string Resumo { get; set; }
        public DateTime DataAutuacao { get; set; }
        public int OrgaoAutuacaoId { get; set; }

        public virtual Assunto Assunto { get; set; }
        public virtual Orgao OrgaoAutuacao { get; set; }
    }
}
