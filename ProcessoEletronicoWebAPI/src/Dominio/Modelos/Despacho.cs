using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Despacho
    {
        public Despacho()
        {
            Anexos = new HashSet<Anexo>();
        }

        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdProcesso { get; set; }
        public int? IdOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public int? IdUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public DateTime DataHoraDespacho { get; set; }
        public Guid GuidOrganizacaoDestino { get; set; }
        public Guid GuidUnidadeDestino { get; set; }

        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
