using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Usuário Autuador")]
        public string NomeUsuarioAutuador { get; set; }
        [Display(Name = "Atividade")]
        public AtividadeViewModel Atividade { get; set; }
        //public List<int> Sinalizacoes { get; set; }

        public OrganizacaoViewModel OrganizacaoProcesso { get; set; }
        [Display(Name = "Municípios Selecionados")]
        public IEnumerable<MunicipioViewModel> MunicipiosRascunhoProcesso { get; set; }
        public List<AtividadeViewModel> AtividadesLista { get; set; }
        public List<UnidadeViewModel> UnidadesLista { get; set; }
        [Display(Name = "UF")]
        public List<UfViewModel> UfLista { get; set; }
        [Display(Name = "Município")]
        public List<MunicipioViewModel> MunicipiosLista { get; set; }
        [Display(Name = "Sinalizações")]
        public List<SinalizacaoViewModel> Sinalizacoes { get; set; }
        public List<SinalizacaoViewModel> SinalizacoesLista { get; set; }

        [Display(Name = "Anexos")]
        public List<AnexoViewModel> Anexos { get; set; }
        public List<IFormFile> files { get; set; }

        public List<InteressadoPessoaFisicaViewModel> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaViewModel> InteressadosPessoaJuridica { get; set; }

        public ListaInteressadosPJPF InteressadosRascunho {
            get {
                return new ListaInteressadosPJPF
                {
                    InteressadosPF = this.InteressadosPessoaFisica,
                    InteressadosPJ = this.InteressadosPessoaJuridica
                };
            }
        }
    }
    public class ListaInteressadosPJPF
    {        
        public List<InteressadoPessoaJuridicaViewModel> InteressadosPJ { get; set; }
        public List<InteressadoPessoaFisicaViewModel> InteressadosPF { get; set; }
    }    
}
