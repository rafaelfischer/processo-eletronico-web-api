using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool AutoSave { get; set; }
        void Save();
        IRepositorioGenerico<T> MakeGenericRepository<T>() where T : class;
    }
}
