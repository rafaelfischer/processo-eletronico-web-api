﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class InteressadoPessoaFisicaModelo
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string GuidMunicipio { get; set; }
        public List<ContatoModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }

    }

    public class InteressadoPessoaFisicaProcessoGetModelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public string GuidMunicipio { get; set; }

        public List<ContatoProcessoGetModelo> Contatos { get; set; }
        public List<EmailModelo> Emails { get; set; }
    }

}
