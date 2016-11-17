using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class TipoContatoModeloNegocio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte QuantidadeDigitos { get; set; }

    }
}
