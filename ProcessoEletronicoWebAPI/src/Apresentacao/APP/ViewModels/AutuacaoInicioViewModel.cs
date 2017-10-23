using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class AutuacaoInicioViewModel
    {
        public int IdRascunho { get; set; }
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string IdAtividade { get; set; }
        public string GuidUnidade { get; set; }
        public string Resumo { get; set; }
        public IEnumerable<int> IdsSinalizacoes { get; set; }
        public OrganizacaoViewModel OrganizacaoUsuario { get; set; }
        public IEnumerable<AtividadeViewModel> Atividades { get; set; }
        public IEnumerable<SinalizacaoViewModel> Sinalizacoes { get; set; }
        public IEnumerable<UnidadeViewModel> Unidades { get; set; }
        public string IdMunicipio { get; set; }
        public IEnumerable<UfViewModel> ListaUfs { get; set; }
        public IEnumerable<MunicipioViewModel> ListaMunicipios { get; set; }
        public string IdUf { get; set; }
    }
}
