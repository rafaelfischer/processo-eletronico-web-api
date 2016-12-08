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
        public int IdOrganizacaoDestino { get; set; }
        [Required]
        public string NomeOrganizacaoDestino { get; set; }
        [Required]
        public string SiglaOrganizacaoDestino { get; set; }
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

    public class DespachoProcessoModeloGet
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public int IdUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string DataHoraDespacho { get; set; }

        public List<AnexoModeloGet> Anexos {get; set;}
                        
    }

    public class DespachoProcessoModeloCompleto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public int IdUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string DataHoraDespacho { get; set; }

        public List<AnexoModeloGet> Anexos { get; set; }

        public ProcessoModelo Processo { get; set; }

    }
}
