using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class DespachoProcessoModeloPost
    {
        [Required]
        public string Texto { get; set; }
        public List<AnexoModelo> Anexos { get; set; } 
        [Required]
        public int IdOrgaoDestino { get; set; }
        [Required]
        public string NomeOrgaoDestino { get; set; }
        [Required]
        public string SiglaOrgaoDestino { get; set; }
        [Required]
        public int IdUnidadeDestino { get; set; }
        [Required]
        public string NomeUnidadeDestino { get; set; }
        [Required]
        public string SiglaUnidadeDestino { get; set; }
        [Required]
        public string IdUsuarioDespachante { get; set; }
        [Required]
        public string NomeUsuarioDespachante { get; set; }
        
    }

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

        public List<AnexoModeloGet> Anexos {get; set;}
                
    }
}
