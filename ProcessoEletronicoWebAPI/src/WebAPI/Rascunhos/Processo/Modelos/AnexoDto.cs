using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{
    public class GetAnexoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string MimeType { get; set; }
        public GetTipoDocumentalDto TipoDocumental { get; set; }
    }

    public class PostAnexoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string MimeType { get; set; }
        public int? IdTipoDocumental { get; set; }
    }

    public class PatchAnexoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public string MimeType { get; set; }
        public int? IdTipoDocumental { get; set; }
    }
}
