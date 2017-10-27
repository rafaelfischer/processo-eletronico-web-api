using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class AnexoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Conteudo { get; set; }
        public string ConteudoString { get; set; }
        public string MimeType { get; set; }
        public TipoDocumentalViewModel TipoDocumental { get; set; }
    }
}
