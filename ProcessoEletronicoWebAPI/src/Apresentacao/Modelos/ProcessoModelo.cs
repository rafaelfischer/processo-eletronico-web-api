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
        public List<AnexoModelo> Anexos { get; set; }
        public List<int> IdSinalizacoes { get; set; }
        public int IdOrgaoAutuador { get; set; }
        [Required]
        public string NomeOrgaoAutuador { get; set; }
        [Required]
        public string SiglaOrgaoAutuador { get; set; }
        [Required]
        public int IdUnidadeAutuadora { get; set; }
        [Required]
        public string NomeUnidadeAutuadora { get; set; }
        [Required]
        public string SiglaUnidadeAutuadora { get; set; }
        [Required]
        public string IdUsuarioAutuador { get; set; }
        [Required]
        public string NomeUsuarioAutuador { get; set; }
    }

    public class ProcessoModelo
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public int IdAtividade { get; set; }
        public int IdOrgaoAutuador { get; set; }
        public string NomeOrgaoAutuador { get; set; }
        public string SiglaOrgaoAutuador { get; set; }
        public int IdUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public string DataAutuacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
        public string Numero { get; set; }
    }

    public class ProcessoCompletoModelo
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public int IdOrgaoAutuador { get; set; }
        public string NomeOrgaoAutuador { get; set; }
        public string SiglaOrgaoAutuador { get; set; }
        public int IdUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public string DataAutuacao { get; set; }
        public string Numero { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public List<AnexoProcessoGetModelo> Anexo { get; set; }
        public List<DespachoProcessoGetModelo> Despacho { get; set; }
        public List<InteressadoPessoaFisicaProcessoGetModelo> InteressadoPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaProcessoGetModelo> InteressadoPessoaJuridica { get; set; }
        public List<MunicipioProcessoModelo> MunicipioProcesso { get; set; }
        public List<SinalizacaoProcessoGetModelo> Sinalizacao { get; set; }
        public AtividadeProcessoGetModelo Atividade { get; set; }
    }
}
