using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class ProcessoModelo
    {
        int IdItemPlanoClassificacao { get; set; }
        string Resumo { get; set; }
        DateTime DataAutuacao { get; set; }
        string Municipio { get; set; }
        string UfMunicipio { get; set; }
        string OrgaoAutuacao { get; set; }
        string siglaOrgaoAutuacao { get; set; }
    }
}
