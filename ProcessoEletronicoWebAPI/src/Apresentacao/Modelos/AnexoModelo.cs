using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class AnexoModelo
    {
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        [Required]
        public string Conteudo { get; set; }
        [Required]
        public string MimeType { get; set; }
        public int? IdTipoDocumental { get; set; }
        
    }

    public class AnexoModeloGet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string MimeType { get; set; }
        public string Conteudo { get; set; }

        public RascunhoProcessoModelo Processo { get; set; }
        public TipoDocumentalAnexoModelo TipoDocumental { get; set; }
    }

    public class AnexoSimplesModeloGet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string MimeType { get; set; }

        public TipoDocumentalAnexoModelo TipoDocumental { get; set; }
    }
}
