using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Publico.Modelos
{
    public class ProcessoApresentacao
    {

        public ProcessoApresentacao() { }
        
        public ProcessoApresentacao(string numero, string digito, string resumo, string assunto, DateTime dataAutuacao, string siglaOrgaoAutuacao)
        {
            this.numero = numero;
            this.digito = digito;
            this.resumo = resumo;
            this.assunto = assunto;
            this.dataAutuacao = dataAutuacao;
            this.siglaOrgaoAutuacao = siglaOrgaoAutuacao;
        }

        public string numero { get; set; }
        public string digito { get; set; }
        public string resumo { get; set; }
        public string assunto { get; set; }
        public DateTime dataAutuacao { get; set; }
        public string siglaOrgaoAutuacao { get; set; }

    }
}
