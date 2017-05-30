using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{


    public class GetContatoDto
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public GetTipoContatoDto TipoContato { get; set; }
    }
    public class PostContatoDto
    {
        public string Telefone { get; set; }
        public int? IdTipoContato { get; set; }
    }

    public class PatchContatoDto
    {
        public string Telefone { get; set; }
        public int? IdTipoContato { get; set; }
    }


}
