using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class TipoDocumentalModelo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdAtividade { get; set; }
    }

    public class TipoDocumentalAnexoModelo
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
