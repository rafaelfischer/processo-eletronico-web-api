using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoAnexoService
    {
        IEnumerable<AnexoViewModel> GetAnexos(int idRascunho);
        AnexoViewModel PostAnexo(int idRascunho, AnexoViewModel anexo);
        void DeleteAnexo(int idRascunho, int idAnexo);
    }
}
