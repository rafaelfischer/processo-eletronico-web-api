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
        public string Descricao { get; set; }
        public byte[] Conteudo { get; set; }
        public string ConteudoString { get; set; }
        public string MimeType { get; set; }
        public TipoDocumentalModeloNegocio TipoDocumental { get; set; }

    }
}
