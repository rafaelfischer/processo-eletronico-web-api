using System;
using System.Collections.Generic;

namespace Apresentacao.WebAPI.Models
{
    public class PatchRascunhoDespachoDto
    {
        public string Texto { get; set; }
        public string GuidOrganizacaoDestino { get; set; }
        public string GuidUnidadeDestino { get; set; }
    }

    public class PostRascunhoDespachoDto : PatchRascunhoDespachoDto
    {
        public IEnumerable<PostRascunhoAnexoDto> Anexos { get; set; }
    }

    public class GetRascunhoDespachoDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataHora { get; set; }
        public string GuidOrganizacaoDestino { get; set; }
        public string GuidUnidadeDestino { get; set; }

        public IEnumerable<GetRascunhoAnexoDto> Anexos { get; set; }

    }
}
