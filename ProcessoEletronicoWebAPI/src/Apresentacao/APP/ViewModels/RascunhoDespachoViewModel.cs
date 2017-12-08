using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class RascunhoDespachoViewModel
    {
        public int Id { get; set; }
        public int IdProcesso { get; set; }
        public int IdAtividade { get; set; }
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
        public int IdOrganizacaoProcesso { get; set; }
        public string GuidOrganizacao { get; set; }

        public IEnumerable<OrganizacaoViewModel> ListaOrganizacoes { get; set; }
        public IEnumerable<UnidadeViewModel> ListaUnidades { get; set; }

        public ICollection<AnexoRascunhoDespachoViewModel> Anexos { get; set; }

        public ListaAnexosRascunhoDespacho AnexosRascunhoDespacho { get; set; }

        public ICollection<TipoDocumentalViewModel> ListaTiposDocumentais { get; set; }
    }

    public class ListaAnexosRascunhoDespacho
    {
        public int IdRascunhoDespacho { get; set; }
        public ICollection<AnexoRascunhoDespachoViewModel> Anexos { get; set; }
        public ICollection<TipoDocumentalViewModel> TiposDocumentais { get; set; }
    }
}
