using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public partial class PlanoClassificacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdOrganizacao { get; set; }
        public bool AreaFim { get; set; }
        public string Observacao { get; set; }

        //public virtual List<Funcao> Funcao { get; set; }
        public virtual OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}
