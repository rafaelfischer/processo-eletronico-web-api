using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class DespachoModeloPost
    {
        [Required]
        public int IdProcesso { get; set; }
        [Required]
        public string Texto { get; set; }
        public List<AnexoModelo> Anexos { get; set; }
        [Required]
        public string GuidOrganizacaoDestino { get; set; }
        [Required]
        public string GuidUnidadeDestino { get; set; }
    }

    public class DespachoModeloGet
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string GuidOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public string GuiUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string DataHoraDespacho { get; set; }
        public List<AnexoModeloGet> Anexos { get; set; }
        public ProcessoModelo Processo { get; set; }
    }

    public class DespachoSimplesModeloGet
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string GuidOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public string GuiUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public string DataHoraDespacho { get; set; }
        public List<AnexoSimplesModeloGet> Anexos { get; set; }
    }
}
