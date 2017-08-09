using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao
{
    public abstract class BaseWorkService : IBaseWorkService
    {
        private Dictionary<string, string> usuario;
        public Dictionary<string, string> Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
                
            }
        }
               
    }
}
