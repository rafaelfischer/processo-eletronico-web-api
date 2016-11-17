using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class TipoContatoModelo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte QuantidadeDigitos { get; set; }

    }
}
