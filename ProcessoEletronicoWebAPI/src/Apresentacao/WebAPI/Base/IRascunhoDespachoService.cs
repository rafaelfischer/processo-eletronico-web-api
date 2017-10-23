using Apresentacao.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.WebAPI.Base
{
    public interface IRascunhoDespachoService
    {
        GetRascunhoDespachoDto Add(PostRascunhoDespachoDto postRascunhoDespachoDto);
        GetRascunhoDespachoDto Search(int id);
        IEnumerable<GetRascunhoDespachoDto> SearchByUsuario(string idUsuario);
        IEnumerable<GetRascunhoDespachoDto> SearchByOrganizacao(string guidOrganizacao);
        void Patch(int id, PatchRascunhoDespachoDto patchRascunhoDespachoDto);
        void Delete(int id);
    }
}
