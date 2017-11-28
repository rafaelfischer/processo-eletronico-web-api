using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoService
    {
        ResultViewModel<RascunhoProcessoViewModel> GetRascunhoProcesso(int id);
        RascunhoProcessoViewModel PostRascunhoProcesso(RascunhoProcessoViewModel rascunhoViewModel);
        IEnumerable<RascunhoProcessoViewModel> GetRascunhosProcessoPorOrganizacao();        
        RascunhoProcessoViewModel EditRascunhoProcesso(int? id);
        ResultViewModel<RascunhoProcessoViewModel> GetForEditRascunhoProcesso(int? id);
        void UpdateRascunhoProcesso(int id, RascunhoProcessoViewModel rascunhoProcessoAlterado);
        ResultViewModel<RascunhoProcessoViewModel> DeleteRascunhoProcesso(int id);
    }
}
