using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class ContatoViewModel
    {
        public int Id { get; set; }
        [DisplayName("Telefone")]        
        public string Telefone { get; set; }

        [DisplayName("Tipo de Contato")]        
        public TipoContatoViewModel TipoContato { get; set; }
    }
}
