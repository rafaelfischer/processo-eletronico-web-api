using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class ProcessoModeloPost
    {
        [Required]
        public int IdAtividade { get; set; }
        [Required]
        public string Resumo { get; set; }
        public List<InteressadoPessoaFisicaModelo> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModelo> InteressadosPessoaJuridica { get; set; }
        [Required]
        public List<MunicipioProcessoModelo> Municipios { get; set; }
        [Required]
        public int IdOrgaoAutuador { get; set; }
        [Required]
        public string OrgaoAutuador { get; set; }
        [Required]
        public string SiglaOrgaoAutuador { get; set; }
        [Required]
        public int IdUnidadeAutuadora { get; set; }
        [Required]
        public string UnidadeAutuadora { get; set; }
        [Required]
        public string SiglaUnidadeAutuadora { get; set; }
        [Required]
        public int IdUsuarioAutuador { get; set; }
        [Required]
        public string UsuarioAutuador { get; set; }
    }
    
}
