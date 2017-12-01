using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class DespachoBasicoViewModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string Destino
        {
            get
            {
                return $"{SiglaOrganizacaoDestino} - {SiglaUnidadeDestino}";
            }
        }
        public DateTime DataHoraDespacho { get; set; }
        public IEnumerable<AnexoBasicoViewModel> Anexos { get; set; }
    }
}
