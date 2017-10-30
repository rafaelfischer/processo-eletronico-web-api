using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoMunicipioService
    {
        IEnumerable<MunicipioViewModel> GetMunicipiosPorIdRascunho(int idRascunho);
        MunicipioViewModel PostMunicipioPorIdRascunho(int idRascunho, MunicipioViewModel municipioViewModel);
    }
}
