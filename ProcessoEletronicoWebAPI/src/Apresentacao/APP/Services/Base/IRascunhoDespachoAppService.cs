using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoDespachoAppService
    {
        ResultViewModel<RascunhoDespachoViewModel> Search(int id);
        ResultViewModel<ICollection<RascunhoDespachoViewModel>> Search();
        ResultViewModel<RascunhoDespachoViewModel> Add(RascunhoDespachoViewModel rascunhoDespacho);
        ResultViewModel<RascunhoDespachoViewModel> Update(int id, RascunhoDespachoViewModel rascunhoDespacho);
        ResultViewModel<RascunhoDespachoViewModel> Delete(int id);
    }
}
