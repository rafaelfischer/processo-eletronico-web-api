using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class MunicipioRascunhoProcesso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public int IdRascunhoProcesso { get; set; }
        public Guid GuidMunicipio { get; set; }

        public virtual RascunhoProcesso RascunhoProcesso { get; set; }
    }
}
