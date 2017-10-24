using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class RascunhoProcessoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Resumo")]
        public string Resumo { get; set; }
        public string GuidOrganizacao { get; set; }
        public string NomeOrganizacao { get; set; }
        public string SiglaOrganizacao { get; set; }
        [Display(Name = "Unidade Responsável")]
        public string GuidUnidade { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        [Display(Name = "Atividade")]
        public AtividadeViewModel Atividade { get; set; }
        //public List<int> Sinalizacoes { get; set; }

        public OrganizacaoViewModel OrganizacaoProcesso { get; set; }
        [Display(Name = "Municípios Selecionados")]
        public List<MunicipioViewModel> MunicipiosRascunhoProcesso { get; set; }
        public List<AtividadeViewModel> AtividadesLista { get; set; }
        public List<UnidadeViewModel> UnidadesLista { get; set; }
        [Display(Name = "UF")]
        public List<UfViewModel> UfLista { get; set; }
        [Display(Name = "Município")]
        public List<MunicipioViewModel> MunicipiosLista { get; set; }
        [Display(Name = "Sinalizações")]
        public List<SinalizacaoViewModel> Sinalizacoes { get; set; }
        public List<SinalizacaoViewModel> SinalizacoesLista { get; set; }

        //public List<AnexoModeloNegocio> Anexos { get; set; }
        //public List<InteressadoPessoaFisicaModeloNegocio> InteressadosPessoaFisica { get; set; }
        //public List<InteressadoPessoaJuridicaModeloNegocio> InteressadosPessoaJuridica { get; set; }
    }
}
