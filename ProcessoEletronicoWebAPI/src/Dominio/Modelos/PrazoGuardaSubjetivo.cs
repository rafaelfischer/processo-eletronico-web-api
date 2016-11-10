using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class PrazoGuardaSubjetivo
    {
        public PrazoGuardaSubjetivo()
        {
            TipoDocumentalIdPrazoGuardaSubjetivoCorrenteNavigation = new HashSet<TipoDocumental>();
            TipoDocumentalIdPrazoGuardaSubjetivoIntermediariaNavigation = new HashSet<TipoDocumental>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<TipoDocumental> TipoDocumentalIdPrazoGuardaSubjetivoCorrenteNavigation { get; set; }
        public virtual ICollection<TipoDocumental> TipoDocumentalIdPrazoGuardaSubjetivoIntermediariaNavigation { get; set; }
    }
}
