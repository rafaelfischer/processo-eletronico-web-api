using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class BaseViewModel
    {
        public ICollection<MensagemViewModel> Mensagens{ get; set; }
    }
}
