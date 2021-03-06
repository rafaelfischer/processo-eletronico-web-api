﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public partial class OrganizacaoProcessoModeloNegocio
    {

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Sigla { get; set; }
        public short DigitoOrganizacao { get; set; }
        public Guid GuidOrganizacao { get; set; }

        public List<PlanoClassificacaoModeloNegocio> PlanoClassificacao { get; set; }

    }
}
