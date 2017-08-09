using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class AnexoRascunho
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Conteudo { get; set; }
        public string MimeType { get; set; }
        public int? IdRascunhoProcesso { get; set; }
        public int? IdTipoDocumental { get; set; }

        public virtual RascunhoProcesso RascunhoProcesso { get; set; }
        public virtual TipoDocumental TipoDocumental { get; set; }
    }
}
