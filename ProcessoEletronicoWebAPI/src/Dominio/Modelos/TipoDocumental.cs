﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class TipoDocumental
    {
        public TipoDocumental()
        {
            Anexos = new HashSet<Anexo>();
            AnexosRascunho = new HashSet<AnexoRascunho>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte? PrazoGuardaAnosCorrente { get; set; }
        public string PrazoGuardaSubjetivoCorrente { get; set; }
        public byte? PrazoGuardaAnosIntermediaria { get; set; }
        public string PrazoGuardaSubjetivoIntermediaria { get; set; }
        public string Observacao { get; set; }
        public int IdDestinacaoFinal { get; set; }
        public int IdAtividade { get; set; }
        public bool Obrigatorio { get; set; }

        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual ICollection<AnexoRascunho> AnexosRascunho { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual DestinacaoFinal DestinacaoFinal { get; set; }
    }
}
