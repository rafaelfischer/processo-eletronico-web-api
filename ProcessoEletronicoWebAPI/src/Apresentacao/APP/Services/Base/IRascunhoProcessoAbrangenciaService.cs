using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoAbrangenciaService
    {
        IEnumerable<MunicipioViewModel> GetMunicipiosPorIdRascunho(int idRascunho);
        MunicipioViewModel PostMunicipioPorIdRascunho(int idRascunho, MunicipioViewModel municipioViewModel);
        ResultViewModel<List<MunicipioViewModel>> UpdateMunicipioPorIdRascunho(int idRascunho, List<string> municipios);
        ResultViewModel<List<MunicipioViewModel>> DeleteAllMunicipio(int idRascunho);
    }
}
