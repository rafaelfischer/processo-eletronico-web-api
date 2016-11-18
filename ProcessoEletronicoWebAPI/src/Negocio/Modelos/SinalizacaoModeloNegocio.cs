using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class SinalizacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public byte[] Imagem { get; set; }

        //public ICollection<SinalizacaoProcesso> SinalizacaoProcesso { get; set; }
        public OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}
