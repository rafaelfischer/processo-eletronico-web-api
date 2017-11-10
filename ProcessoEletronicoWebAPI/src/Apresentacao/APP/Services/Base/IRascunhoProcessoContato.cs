using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoContato
    {
        List<TipoContatoViewModel> GetTiposContato();
    }
}
