﻿using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class InteressadoPessoaFisicaModeloNegocio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public List<ContatoModeloNegocio> Contatos { get; set; }
        public List<EmailModeloNegocio> Emails { get; set; }
    }
}

