using Apresentacao.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.WebAPI.Base
{
    public interface IAnexoRascunhoDespachoService
    {
        GetRascunhoAnexoDto Search(int idRascunhoDespacho, int id);
        IEnumerable<GetRascunhoAnexoDto> Search(int idRascunhoDespacho);
        GetRascunhoAnexoDto Add(int idRascunhoDespacho, PostRascunhoAnexoDto postRascunhoAnexoDto);
        void Update(int idRascunhoDespacho, int id, PatchRascunhoAnexoDto patchRascunhoAnexoDto);
        void Delete(int idRascunhoDespacho, int id);
    }
}

