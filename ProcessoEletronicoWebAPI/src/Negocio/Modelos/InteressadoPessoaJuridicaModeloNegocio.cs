using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class InteressadoPessoaJuridicaModeloNegocio
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }

        public List<ContatoModeloNegocio> Contatos { get; set; }
        public List<EmailModeloNegocio> Emails { get; set; }
        public ProcessoModeloNegocio Processo { get; set; }
    }
}
