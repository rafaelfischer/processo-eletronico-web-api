using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Publico.Modelos
{
    public class ProcessoApresentacao
    {
        public string numero { get; set; }
        public string digito { get; set; }
        public string resumo { get; set; }
        public string assunto { get; set; }
        public DateTime dataAutuacao { get; set; }
        public string siglaOrgaoAutuacao { get; set; }

    }
}
