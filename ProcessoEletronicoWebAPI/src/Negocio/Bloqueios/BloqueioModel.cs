using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Bloqueios
{
    public class BloqueioModel
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string CpfUsuario { get; set; }
        public string NomeSistema { get; set; }
        public string Motivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
