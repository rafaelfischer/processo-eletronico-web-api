using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class EmailRascunho
    {
        public int Id { get; set; }
        public string Endereco { get; set; }
        public int? IdInteressadoPessoaFisicaRascunho { get; set; }
        public int? IdInteressadoPessoaJuridicaRascunho { get; set; }

        public virtual InteressadoPessoaFisicaRascunho InteressadoPessoaFisicaRascunho { get; set; }
        public virtual InteressadoPessoaJuridicaRascunho InteressadoPessoaJuridicaRascunho { get; set; }
    }
}
