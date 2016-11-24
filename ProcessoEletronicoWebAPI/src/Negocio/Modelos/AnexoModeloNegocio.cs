using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class AnexoModeloNegocio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Conteudo { get; set; }
        public string Tipo { get; set; }
        public int IdProcesso { get; set; }
        public int? IdTipoDocumental { get; set; }
        public int? IdDespacho { get; set; }

        public virtual DespachoModeloNegocio Despacho { get; set; }
        public virtual ProcessoModeloNegocio Processo { get; set; }
        public virtual TipoDocumentalModeloNegocio TipoDocumental { get; set; }
    }
}
