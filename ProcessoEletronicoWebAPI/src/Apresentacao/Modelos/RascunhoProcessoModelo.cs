using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Modelos
{
    public class GetRascunhoProcessoDto
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public string GuidOrganizacao { get; set; }
        public string NomeOrganizacao { get; set; }
        public string SiglaOrganizacao { get; set; }
        public string GuidUnidade { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public List<AnexoSimplesModeloGet> Anexos { get; set; }
        public List<GetInteressadoPessoaFisicaDto> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaProcessoGetModelo> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloGet> MunicipiosProcesso { get; set; }
        public List<SinalizacaoProcessoGetModelo> Sinalizacoes { get; set; }
        public AtividadeProcessoGetModelo Atividade { get; set; }
    }

    public class GetRascunhoProcessoPorOrganizacaoDto
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public int IdAtividade { get; set; }
        public string GuidOrganizacao { get; set; }
        public string NomeOrganizacao { get; set; }
        public string SiglaOrganizacao { get; set; }
        public string GuidUnidade { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
    }

    public class PostRascunhoProcessoDto
    {
        public int? IdAtividade { get; set; }
        public string Resumo { get; set; }
        public List<PostInteressadoPessoaFisicaDto> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModelo> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloPost> MunicipiosRascunhoProcesso { get; set; }
        public List<AnexoModelo> Anexos { get; set; }
        public List<int> IdSinalizacoes { get; set; }
        [Required]
        public string GuidOrganizacao { get; set; }
        [Required]
        public string GuidUnidade { get; set; }
    }

    public class PatchRascunhoProcessoDto
    {
        public int? IdAtividade { get; set; }
        public string Resumo { get; set; }
        [Required]
        public string GuidOrganizacao { get; set; }
        [Required]
        public string GuidUnidade { get; set; }
    }
}
