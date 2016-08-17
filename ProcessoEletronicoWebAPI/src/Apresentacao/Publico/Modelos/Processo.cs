using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Publico.Modelos
{
    public class Processo
    {

        public Processo()
        {
            assunto = new Assunto();
            orgaoAutuacao = new Orgao();
        }

        string numero { get; set; }
        string digito { get; set; }
        string resumo { get; set; }
        Assunto assunto { get; set; }
        DateTime dataAutuacao { get; set; }
        Orgao orgaoAutuacao { get; set; }

    }

    public class Assunto
    {
        int id { get; set; }
        string descricao { get; set; }

    }

    public class Orgao
    {
        int id { get; set; }
        string sigla { get; set; }
    }
}
