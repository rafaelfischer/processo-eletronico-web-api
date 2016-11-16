using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public partial class FuncaoModeloNegocio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        //public virtual ICollection<Atividade> Atividade { get; set; }
        public FuncaoModeloNegocio FuncaoPai { get; set; }
        public ICollection<FuncaoModeloNegocio> FuncoesFilhas { get; set; }
        public PlanoClassificacaoModeloNegocio PlanoClassificacao { get; set; }
    }
}
