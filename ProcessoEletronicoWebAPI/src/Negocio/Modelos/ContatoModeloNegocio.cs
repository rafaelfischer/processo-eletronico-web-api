using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class ContatoModeloNegocio
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public TipoContatoModeloNegocio TipoContato { get; set; }
        public InteressadoPessoaFisicaModeloNegocio InteressadoPessoaFisica { get; set; }
        public InteressadoPessoaJuridicaModeloNegocio InteressadoPessoaJuridica { get; set; }
    }
}
