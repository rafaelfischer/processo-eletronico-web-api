using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class MunicipioViewModel
    {
        public string guid { get; set; }
        public int codigoIbge { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }

        public string id { get { return this.guid; } }
        public string text{ get { return this.nome; } }
    }
}
