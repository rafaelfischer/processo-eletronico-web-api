using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoService
    {
        IEnumerable<RascunhoProcessoViewModel> GetRascunhosProcessoPorOrganizacao();        
        RascunhoProcessoViewModel EditRascunhoProcesso(int? id);        
        void UpdateRascunhoProcesso(int id, RascunhoProcessoViewModel rascunhoProcessoAlterado);
        void DeleteRascunhoProcesso(int id);
    }
}
