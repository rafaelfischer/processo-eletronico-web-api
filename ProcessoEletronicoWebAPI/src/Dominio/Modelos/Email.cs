using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Email
    {
        public int Id { get; set; }
        public string Endereco { get; set; }
        public int? IdInteressadoPessoaFisica { get; set; }
        public int? IdInteressadoPessoaJuridica { get; set; }

        public virtual InteressadoPessoaFisica InteressadoPessoaFisica { get; set; }
        public virtual InteressadoPessoaJuridica InteressadoPessoaJuridica { get; set; }
    }
}
