﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class DigitoEsfera
    {
        public DigitoEsfera()
        {
            OrganizacaoProcesso = new HashSet<OrganizacaoProcesso>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte Digito { get; set; }

        public virtual ICollection<OrganizacaoProcesso> OrganizacaoProcesso { get; set; }
    }
}
