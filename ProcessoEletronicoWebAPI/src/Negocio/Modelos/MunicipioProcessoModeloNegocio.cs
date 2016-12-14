using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class MunicipioProcessoModeloNegocio
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string GuidMunicipio { get; set; }
    }
}
