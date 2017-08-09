using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum.Base
{
    //Ao implementar, utilizar T como Modelo de negócio e U como Modelo de domínio
    public interface IBaseValidation<T, U>
    {
        void Exists(U u);
        void IsFilled(T t);
        void IsValid(T t);
    }
}
