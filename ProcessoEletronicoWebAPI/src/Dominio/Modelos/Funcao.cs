using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Funcao
    {
        public Funcao()
        {
            Atividade = new HashSet<Atividade>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int IdPlanoClassificacao { get; set; }
        public int? IdFuncaoPai { get; set; }

        public virtual ICollection<Atividade> Atividade { get; set; }
        public virtual Funcao IdFuncaoPaiNavigation { get; set; }
        public virtual ICollection<Funcao> InverseIdFuncaoPaiNavigation { get; set; }
        public virtual PlanoClassificacao IdPlanoClassificacaoNavigation { get; set; }
    }
}
