using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class ProcessoNegocio
    {
        public ProcessoNegocio()
        {
            assunto = new Assunto();
            orgaoAutuacao = new Orgao();
        }
        
        public ProcessoNegocio(string numero, string digito, string resumo, int assuntoId, string assuntoDescricao, DateTime dataAutuacao, int orgaoID, string orgaoSigla)
        {
            this.assunto = new Assunto();
            this.orgaoAutuacao = new Orgao();

            this.numero = numero;
            this.digito = digito;
            this.resumo = resumo;
            assunto.id = assuntoId;
            assunto.descricao = assuntoDescricao;
            this.dataAutuacao = dataAutuacao;
            orgaoAutuacao.id = orgaoID;
            orgaoAutuacao.sigla = orgaoSigla;

        }

        public string numero { get; set; }
        public string digito { get; set; }
        public string resumo { get; set; }
        public Assunto assunto { get; set; }
        public DateTime dataAutuacao { get; set; }
        public Orgao orgaoAutuacao { get; set; }

    }

    public class Assunto
    {
        public int id { get; set; }
        public string descricao { get; set; }

    }

    public class Orgao
    {
        public int id { get; set; }
        public string sigla { get; set; }
    }
}
