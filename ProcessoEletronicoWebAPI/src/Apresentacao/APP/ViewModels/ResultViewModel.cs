using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class ResultViewModel<T>
    {
        public T Entidade { get; set; }
        public bool Success { get; set; }
        public ICollection<MensagemViewModel> Mensagens { get; set; }
    }
}
