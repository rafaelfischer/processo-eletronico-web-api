using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{  
    public class UnidadeViewModel
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public Tipounidade tipoUnidade { get; set; }
        public Unidadepai unidadePai { get; set; }
    }

    public class Tipounidade
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }

    public class Unidadepai
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
    }

}
