using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Contato
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public int IdTipoContato { get; set; }
        public int? IdInteressadoPessoaFisica { get; set; }
        public int? IdInteressadoPessoaJuridica { get; set; }

        public virtual InteressadoPessoaFisica IdInteressadoPessoaFisicaNavigation { get; set; }
        public virtual InteressadoPessoaJuridica IdInteressadoPessoaJuridicaNavigation { get; set; }
        public virtual TipoContato IdTipoContatoNavigation { get; set; }
    }
}
