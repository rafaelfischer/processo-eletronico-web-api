using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Restrito.Modelos
{
    /*Classe que receberá os dados vindo dos JSON com informações do processo a ser autuado (HTTP POST)*/
    public class ProcessoAutuacao
    {
        public string AssuntoId { get; set; }
        public string Resumo { get; set; }
        public string OrgaoAutuacaoId { get; set; }
    }
}
