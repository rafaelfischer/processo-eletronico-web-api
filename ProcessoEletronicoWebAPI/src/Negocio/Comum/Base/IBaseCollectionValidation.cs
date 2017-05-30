using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum.Base
{
    public interface IBaseCollectionValidation<T>
    {
        void IsFilled(IEnumerable<T> t);
        void IsValid(IEnumerable<T> t);
    }
}
