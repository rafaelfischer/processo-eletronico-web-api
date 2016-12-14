using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class PlanoClassificacao
    {
        public PlanoClassificacao()
        {
            Funcoes = new HashSet<Funcao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool AreaFim { get; set; }
        public string Observacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public virtual ICollection<Funcao> Funcoes { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
