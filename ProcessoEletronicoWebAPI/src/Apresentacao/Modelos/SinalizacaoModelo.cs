using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class SinalizacaoModelo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public byte[] Imagem { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
    }
}
