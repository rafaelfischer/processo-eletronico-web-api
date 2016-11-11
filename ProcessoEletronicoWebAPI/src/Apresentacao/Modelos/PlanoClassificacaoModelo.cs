using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public partial class PlanoClassificacaoModelo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdOrganizacao { get; set; }
        public bool AreaFim { get; set; }
        public string Observacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
    }
}
