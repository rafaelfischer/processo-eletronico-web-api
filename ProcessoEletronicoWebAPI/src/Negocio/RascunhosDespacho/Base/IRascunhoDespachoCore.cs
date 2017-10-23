using Negocio.RascunhosDespacho.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.RascunhosDespacho.Base
{
    public interface IRascunhoDespachoCore
    {
        RascunhoDespachoModel Search(int id);
        IEnumerable<RascunhoDespachoModel> SearchByOrganizacao(int id);
        IEnumerable<RascunhoDespachoModel> SearchByUsuario(int id);
        RascunhoDespachoModel Add(RascunhoDespachoModel rascunhoDespacho);
        void Update(int id, RascunhoDespachoModel rascunhoDespacho);
        void Delete(int id);
    }
}
