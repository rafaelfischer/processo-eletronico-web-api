using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class SinalizacaoViewModel
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string cor { get; set; }
        public string imagem { get; set; }
        public int idOrganizacaoProcesso { get; set; }
    }
}
