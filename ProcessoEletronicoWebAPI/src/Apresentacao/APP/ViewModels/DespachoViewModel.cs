using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.ViewModels
{
    public class GetDespachoBasicoViewModel
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

    public class GetDespachoViewModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }

        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
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
        public ICollection<AnexoViewModel> Anexos { get; set; }
        public GetProcessoBasicoViewModel Processo { get; set; }
    }
}
