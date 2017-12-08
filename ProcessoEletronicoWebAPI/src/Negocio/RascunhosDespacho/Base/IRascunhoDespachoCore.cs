using Negocio.RascunhosDespacho.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.RascunhosDespacho.Base
{
    public interface IRascunhoDespachoCore
    {
        RascunhoDespachoModel Search(int id);
        IEnumerable<RascunhoDespachoModel> SearchByOrganizacao();
        IEnumerable<RascunhoDespachoModel> SearchByUsuario();
        RascunhoDespachoModel Add(RascunhoDespachoModel rascunhoDespacho);
        RascunhoDespachoModel Clone(int id);
        void Update(int id, RascunhoDespachoModel rascunhoDespacho);
        void Delete(int id);
    }
}
