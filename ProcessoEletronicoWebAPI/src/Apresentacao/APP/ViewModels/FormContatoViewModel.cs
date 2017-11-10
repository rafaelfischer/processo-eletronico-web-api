using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class FormContatoViewModel
    {
        public ContatoViewModel contato { get; set; }
        public List<TipoContatoViewModel> TiposContato { get; set; }
    }
}
