﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public partial class OrganizacaoProcessoModeloNegocio
    {
        public int Id { get; set; }
        public int IdOrganizacao { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public short NumeroOrganiacao { get; set; }

        public virtual ICollection<PlanoClassificacaoModeloNegocio> PlanoClassificacao { get; set; }
        //public virtual ICollection<Processo> Processo { get; set; }
        //public virtual ICollection<Sinalizacao> Sinalizacao { get; set; }
        //public virtual DigitoEsfera IdDigitoEsferaNavigation { get; set; }
        //public virtual DigitoPoder IdDigitoPoderNavigation { get; set; }
    }
}