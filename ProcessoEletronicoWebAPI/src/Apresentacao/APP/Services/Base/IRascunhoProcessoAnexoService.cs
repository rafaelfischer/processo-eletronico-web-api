using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoAnexoService
    {
        ICollection<AnexoViewModel> GetAnexos(int idRascunho);
        AnexoViewModel GetAnexo(int idRascunho, int idAnexo);
        AnexoViewModel PostAnexo(int idRascunho, AnexoViewModel anexo);
        void EditarAnexo(int idRascunho, AnexoViewModel anexo);
        void DeleteAnexo(int idRascunho, int idAnexo);
    }
}
