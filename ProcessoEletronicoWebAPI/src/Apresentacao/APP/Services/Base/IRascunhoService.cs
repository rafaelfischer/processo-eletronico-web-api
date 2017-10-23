using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoService
    {
        RascunhoProcessoModeloNegocio PostRascunho();
        IEnumerable<GetRascunhoProcessoViewModel> GetRascunhosOrganizacao();
        AutuacaoInicioViewModel GetFormularioInicioAutuacao();        
    }
}
