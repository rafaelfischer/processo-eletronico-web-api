using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.WebAPI.Models
{
    public class PostRascunhoAnexoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string MimeType { get; set; }
        public int IdTipoDocumental { get; set; }
    }

    public class PatchRascunhoAnexoDto : PostRascunhoAnexoDto
    {
    }

    public class GetRascunhoAnexoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string MimeType { get; set; }
        public int IdTipoDocumental { get; set; }
    }
}
