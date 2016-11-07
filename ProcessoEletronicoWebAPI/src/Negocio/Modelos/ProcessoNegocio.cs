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
