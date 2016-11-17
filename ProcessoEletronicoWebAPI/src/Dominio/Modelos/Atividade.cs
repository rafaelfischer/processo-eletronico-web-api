using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Atividade
    {
        public Atividade()
        {
            Processo = new HashSet<Processo>();
            TipoDocumental = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int IdFuncao { get; set; }

        public virtual ICollection<Processo> Processo { get; set; }
        public virtual ICollection<TipoDocumental> TipoDocumental { get; set; }
        public virtual Funcao Funcao { get; set; }
    }
}
