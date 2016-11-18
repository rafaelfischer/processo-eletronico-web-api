using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class PrazoGuardaSubjetivo
    {
        public PrazoGuardaSubjetivo()
        {
            TipoDocumentalIdPrazoGuardaSubjetivoCorrente = new HashSet<TipoDocumental>();
            TipoDocumentalIdPrazoGuardaSubjetivoIntermediaria = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<TipoDocumental> TipoDocumentalIdPrazoGuardaSubjetivoCorrente { get; set; }
        public virtual ICollection<TipoDocumental> TipoDocumentalIdPrazoGuardaSubjetivoIntermediaria { get; set; }
    }
}
