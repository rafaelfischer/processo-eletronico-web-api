using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class ResultViewModel
    {
        public object Entidade { get; set; }                
        public ICollection<object> Entidades {get; set;}
        public ICollection<MensagemViewModel> Mensagens { get; set; }
    }
}
