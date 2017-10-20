using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.RascunhosDespacho.Models
{
    public class RascunhoDespachoModel
    {

        public RascunhoDespachoModel()
        {
            Anexos = new List<AnexoModeloNegocio>();
        }

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
        public int IdOrganizacaoProcesso { get; set; }

        public IList<AnexoModeloNegocio> Anexos { get; set; }
    }
}
