using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Bloqueios
{
    public class GetBloqueioDto
    {
        public int Id { get; set; }
        public string CpfUsuario { get; set; }
        public string NomeSistema { get; set; }
        public string Motivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }

    public class PostBloqueioDto
    {
        public string Motivo { get; set; }
    }
    
}
