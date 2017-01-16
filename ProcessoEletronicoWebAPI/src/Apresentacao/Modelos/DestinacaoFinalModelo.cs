using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{

    public class DestinacaoFinalModeloPost
    {
        public string Descricao { get; set; }
    }

    public class DestinacaoFinalModeloGet : DestinacaoFinalModeloPost
    {
        public int Id { get; set; }
       
    }
}
