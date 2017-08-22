using ProcessoEletronicoService.WebAPI.Sinalizacoes.Modelos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
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
        public GetAtividadeDto Atividade { get; set; }
        public List<GetAnexoDto> Anexos { get; set; }
        public List<GetInteressadoPessoaFisicaDto> InteressadosPessoaFisica { get; set; }
        public List<GetInteressadoPessoaJuridicaDto> InteressadosPessoaJuridica { get; set; }
        public List<GetMunicipioDto> Municipios { get; set; }
        public List<GetSinalizacaoNoImagemDto> Sinalizacoes { get; set; }

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
        public List<PostInteressadoPessoaJuridicaDto> InteressadosPessoaJuridica { get; set; }
        public List<PostMunicipioDto> Municipios { get; set; }
        public List<PostAnexoDto> Anexos { get; set; }
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
