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
        List<MunicipioViewModel> UpdateMunicipioPorIdRascunho(int idRascunho, List<string> municipios);
        void DeleteAllMunicipio(int idRascunho);
    }
}
