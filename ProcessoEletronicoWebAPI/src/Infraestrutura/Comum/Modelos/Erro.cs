using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Comum.Modelos
{
    public class Erro
    {
        public Erro () { }

        public Erro (int codigo, string mensagem)
        {
            this.codigo = codigo;
            this.mensagem = mensagem;
        }

        int codigo { get; set; }
        string mensagem { get; set; }

    }
}
