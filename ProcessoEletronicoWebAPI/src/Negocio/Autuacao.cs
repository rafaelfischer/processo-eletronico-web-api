using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio
{
    public class Autuacao : IAutuacao
    {
        public string Autuar()
        {
            return "Funcionou!";
        }
    }
}
