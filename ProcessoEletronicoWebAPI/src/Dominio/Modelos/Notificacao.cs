using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public class Notificacao
    {
        public int Id { get; set; }
        public int IdProcesso { get; set; }
        public int? IdDespacho { get; set; }
        public string Email { get; set; }
        public DateTime? DataNotificacao { get; set; }

        public virtual Despacho Despacho { get; set; }
        public virtual Processo Processo { get; set; }
    }
}
