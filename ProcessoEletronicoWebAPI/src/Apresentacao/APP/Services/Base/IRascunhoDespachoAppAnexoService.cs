using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoDespachoAppAnexoService
    {
        ResultViewModel<AnexoRascunhoDespachoViewModel> Search(int idRascunhoDespacho, int id);
        ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>> Search(int idRascunhoDespacho);
        ResultViewModel<AnexoRascunhoDespachoViewModel> Add(int idRascunhoDespacho, AnexoRascunhoDespachoViewModel anexoRascunhoDespacho);
        ResultViewModel<AnexoRascunhoDespachoViewModel> Update(int idRascunhoDespacho, int id, AnexoRascunhoDespachoViewModel anexoRascunhoDespacho);
        ResultViewModel<AnexoRascunhoDespachoViewModel> Delete(int idRascunhoDespacho, int id);
    }
}
