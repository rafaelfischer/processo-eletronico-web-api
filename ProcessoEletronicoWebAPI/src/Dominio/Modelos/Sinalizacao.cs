using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Sinalizacao
    {
        public Sinalizacao()
        {
            SinalizacaoProcesso = new HashSet<SinalizacaoProcesso>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public byte[] Imagem { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public virtual ICollection<SinalizacaoProcesso> SinalizacaoProcesso { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
