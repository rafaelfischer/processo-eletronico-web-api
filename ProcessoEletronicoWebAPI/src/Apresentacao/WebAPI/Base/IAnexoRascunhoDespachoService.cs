using Apresentacao.WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        void Patch(int idRascunhoDespacho, int id, JsonPatchDocument<PatchRascunhoAnexoDto> patchRascunhoAnexoDto);
        void Delete(int idRascunhoDespacho, int id);
    }
}

