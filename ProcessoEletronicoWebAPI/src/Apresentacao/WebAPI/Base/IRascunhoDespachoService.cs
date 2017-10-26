using Apresentacao.WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace Apresentacao.WebAPI.Base
{
    public interface IRascunhoDespachoService
    {
        GetRascunhoDespachoDto Add(PostRascunhoDespachoDto postRascunhoDespachoDto);
        GetRascunhoDespachoDto Search(int id);
        IEnumerable<GetRascunhoDespachoDto> SearchByUsuario(string idUsuario);
        IEnumerable<GetRascunhoDespachoDto> SearchByOrganizacao(string guidOrganizacao);
        void Patch(int id, JsonPatchDocument<PatchRascunhoDespachoDto> jsonPatchRascunhoDespachoDto);
        void Delete(int id);
    }
}
