using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class ContatoViewModel
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public TipoContatoViewModel TipoContato { get; set; }
    }
}
