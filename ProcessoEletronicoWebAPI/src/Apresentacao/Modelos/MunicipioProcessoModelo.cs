using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class MunicipioProcessoModeloPost
    {
        [Required]
        public string GuidMunicipio { get; set; }

    }
    
    public class MunicipioProcessoModeloGet : MunicipioProcessoModeloPost
    {
        public string Uf { get; set; }
        public string Nome { get; set; }
    }

}
