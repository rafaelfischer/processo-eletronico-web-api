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

    public class ListaAnexosRascunho
    {
        public int IdRascunho { get; set; }
        public ICollection<AnexoViewModel> Anexos { get; set; }
    }

    public class EditarAnexoRascunho
    {
        public int IdRascunho { get; set; }
        public AnexoViewModel Anexo { get; set; }
        public IEnumerable<TipoDocumentalViewModel> TiposDocumentais { get; set; }
    }

    public class EditarAnexosRascunho
    {
        public int IdRascunho { get; set; }
        public IEnumerable<AnexoViewModel> Anexos { get; set; }
        public IEnumerable<TipoDocumentalViewModel> TiposDocumentais { get; set; }
    }
}
