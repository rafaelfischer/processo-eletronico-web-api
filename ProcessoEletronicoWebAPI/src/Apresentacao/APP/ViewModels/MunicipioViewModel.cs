using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class MunicipioViewModel
    {
        public string GuidMunicipio { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }        

        public string Id { get { return this.GuidMunicipio; }}
        public string Text { get { return this.Nome; } }
    }
}
