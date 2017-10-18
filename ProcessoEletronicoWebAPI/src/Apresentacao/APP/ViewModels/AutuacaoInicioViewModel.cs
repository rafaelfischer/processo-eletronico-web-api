using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class AutuacaoInicioViewModel
    {
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string IdAtividade { get; set; }
        public string GuidUnidade { get; set; }
        public string Resumo { get; set; }
        public List<int> IdsSinalizacoes { get; set; }
        public OrganizacaoViewModel OrganizacaoUsuario { get; set; }
        public List<AtividadeViewModel> Atividades { get; set; }
        public List<SinalizacaoViewModel> Sinalizacoes { get; set; }
        public List<UnidadeViewModel> Unidades { get; set; }
    }
}
