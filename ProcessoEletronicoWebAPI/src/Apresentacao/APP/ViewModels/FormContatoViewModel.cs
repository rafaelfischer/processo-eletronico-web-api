using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class FormContatoViewModel
    {
        public List<ContatoViewModel> Contatos { get; set; }
        public List<TipoContatoViewModel> TiposContato { get; set; }
    }
}
