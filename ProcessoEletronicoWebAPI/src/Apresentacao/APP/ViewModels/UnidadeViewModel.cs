using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{  
    public class UnidadeViewModel
    {
        public string Guid { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }          
        public string NomeSigla
        {
            get
            {
                return this.Sigla + " - " + this.Nome;
            }
        }
        public string Text
        {
            get
            {
                return this.Sigla + " - " + this.Nome;
            }
        }

        public string Id
        {
            get
            {
                return this.Guid;
            }
        }
    }    
}
