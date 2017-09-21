using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class ProcessoConsultaViewModel
    {
        public int id { get; set; }
        public string resumo { get; set; }
        public string guidOrganizacaoAutuadora { get; set; }
        public string nomeOrganizacaoAutuadora { get; set; }
        public string siglaOrganizacaoAutuadora { get; set; }
        public string guidUnidadeAutuadora { get; set; }
        public string nomeUnidadeAutuadora { get; set; }
        public string siglaUnidadeAutuadora { get; set; }
        public string idUsuarioAutuador { get; set; }
        public string nomeUsuarioAutuador { get; set; }
        public string dataAutuacao { get; set; }
        public string dataUltimoTramite { get; set; }
        public string numero { get; set; }
        public int idOrganizacaoProcesso { get; set; }     
    }
}
