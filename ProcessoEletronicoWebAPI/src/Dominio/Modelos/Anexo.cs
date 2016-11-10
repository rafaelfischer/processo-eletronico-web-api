using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Anexo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Conteudo { get; set; }
        public string Tipo { get; set; }
        public int IdProcesso { get; set; }
        public int? IdTipoDocumental { get; set; }

        public virtual Processo IdProcessoNavigation { get; set; }
        public virtual TipoDocumental IdTipoDocumentalNavigation { get; set; }
    }
}
