using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class RascunhoProcessoModeloNegocio
    {

        public int Id { get; set; }
        public string Resumo { get; set; }
        public string GuidOrganizacao { get; set; }
        public string NomeOrganizacao { get; set; }
        public string SiglaOrganizacao { get; set; }
        public string GuidUnidade { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
       

        public List<AnexoModeloNegocio> Anexos { get; set; }
        public List<InteressadoPessoaFisicaModeloNegocio> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModeloNegocio> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioRascunhoProcessoModeloNegocio> MunicipiosRascunhoProcesso { get; set; }
        public List<SinalizacaoModeloNegocio> Sinalizacoes { get; set; }
        public AtividadeModeloNegocio Atividade { get; set; }
        public OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}