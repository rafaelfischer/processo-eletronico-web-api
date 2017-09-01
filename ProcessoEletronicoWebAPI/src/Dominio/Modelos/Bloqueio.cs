using System;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Bloqueio
    {
        public int Id { get; set; }
        public int IdProcesso { get; set; }
        public string NomeUsuario { get; set; }
        public string CpfUsuario { get; set; }
        public string NomeSistema { get; set; }
        public string Motivo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public virtual Processo Processo { get; set; }
    }
}
