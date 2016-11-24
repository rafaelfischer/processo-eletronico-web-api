using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class DespachoProcessoGetModelo
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdOrgaoDestino { get; set; }
        public string NomeOrgaoDestino { get; set; }
        public string SiglaOrgaoDestino { get; set; }
        public int IdUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string DataHoraDespacho { get; set; }

        public ProcessoModelo Processo { get; set; }
    }
}
