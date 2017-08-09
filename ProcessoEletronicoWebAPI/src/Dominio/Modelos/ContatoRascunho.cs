using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class ContatoRascunho
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public int? IdTipoContato { get; set; }
        public int? IdInteressadoPessoaFisicaRascunho { get; set; }
        public int? IdInteressadoPessoaJuridicaRascunho { get; set; }

        public virtual InteressadoPessoaFisicaRascunho InteressadoPessoaFisicaRascunho { get; set; }
        public virtual InteressadoPessoaJuridicaRascunho InteressadoPessoaJuridicaRascunho { get; set; }
        public virtual TipoContato TipoContato { get; set; }
    }
}
