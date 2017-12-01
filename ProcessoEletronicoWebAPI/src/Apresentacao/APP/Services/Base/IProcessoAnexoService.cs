using Apresentacao.APP.ViewModels;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IProcessoAnexoService
    {
        AnexoViewModel Search(int id);
    }
}
