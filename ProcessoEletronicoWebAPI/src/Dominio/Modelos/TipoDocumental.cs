using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class TipoDocumental
    {
        public TipoDocumental()
        {
            Anexo = new HashSet<Anexo>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte? PrazoGuardaAnosCorrente { get; set; }
        public int? IdPrazoGuardaSubjetivoCorrente { get; set; }
        public byte? PrazoGuardaAnosIntermediaria { get; set; }
        public int? IdPrazoGuardaSubjetivoIntermediaria { get; set; }
        public string Observacao { get; set; }
        public int IdDestinacaoFinal { get; set; }
        public int IdAtividade { get; set; }
        public bool Obrigatorio { get; set; }

        public virtual ICollection<Anexo> Anexo { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual DestinacaoFinal DestinacaoFinal { get; set; }
        public virtual PrazoGuardaSubjetivo PrazoGuardaSubjetivoCorrente { get; set; }
        public virtual PrazoGuardaSubjetivo PrazoGuardaSubjetivoIntermediaria { get; set; }
    }
}
