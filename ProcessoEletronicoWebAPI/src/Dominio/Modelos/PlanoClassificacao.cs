using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class PlanoClassificacao
    {
        public PlanoClassificacao()
        {
            Funcao = new HashSet<Funcao>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdOrganizacao { get; set; }
        public bool AreaFim { get; set; }
        public string Observacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public virtual ICollection<Funcao> Funcao { get; set; }
        public virtual OrganizacaoProcesso IdOrganizacaoProcessoNavigation { get; set; }
    }
}
