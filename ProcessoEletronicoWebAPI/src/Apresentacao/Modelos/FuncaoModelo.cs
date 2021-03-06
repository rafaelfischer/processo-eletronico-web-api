﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{

    public class FuncaoModeloPost
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int IdPlanoClassificacao { get; set; }
        public int? IdFuncaoPai { get; set; }
    }

    public class FuncaoModelo : FuncaoModeloPost
    {
        public int Id { get; set; }
       
    }
    public partial class FuncaoProcessoGetModelo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public PlanoClassificacaoProcessoGetModelo PlanoClassificacao { get; set; }
    }
    
}
