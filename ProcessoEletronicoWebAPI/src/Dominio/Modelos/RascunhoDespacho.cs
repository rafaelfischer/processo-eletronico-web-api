using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class RascunhoDespacho
    {
        public RascunhoDespacho()
        {
            AnexosRascunho = new HashSet<AnexoRascunho>();
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
        public Guid? GuidOrganizacaoDestino { get; set; }
        public Guid? GuidUnidadeDestino { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public virtual ICollection<AnexoRascunho> AnexosRascunho { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
