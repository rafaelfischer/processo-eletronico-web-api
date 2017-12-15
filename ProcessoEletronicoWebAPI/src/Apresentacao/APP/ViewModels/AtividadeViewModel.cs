using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public partial class AtividadeViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoDescricao { get { return this.Codigo + " - " + this.Descricao; } }
    }
}
