using Negocio.RascunhosDespacho.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.RascunhosDespacho.Base
{
    public interface IAnexoRascunhoDespachoCore
    {
        AnexoRascunhoDespachoModel Search(int idRascunhoDespacho, int id);
        IEnumerable<AnexoRascunhoDespachoModel> Search(int idRascunhoDespacho);
        AnexoRascunhoDespachoModel Add(int idRascunhoDespacho, AnexoRascunhoDespachoModel anexoRascunhoDespacho);
        void Update(int idRascunhoDespacho, int id, AnexoRascunhoDespachoModel rascunhoDespacho);
        void Delete(int idRascunhoDespacho, int id);
    }
}
