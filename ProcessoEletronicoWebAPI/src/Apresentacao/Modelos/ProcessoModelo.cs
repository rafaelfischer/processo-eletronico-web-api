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
        public List<PostInteressadoPessoaFisicaDto> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModelo> InteressadosPessoaJuridica { get; set; }
        [Required]
        public List<MunicipioProcessoModeloPost> Municipios { get; set; }
        public List<AnexoModelo> Anexos { get; set; }
        public List<int> IdSinalizacoes { get; set; }
        [Required]
        public string GuidOrganizacaoAutuadora { get; set; }
        [Required]
        public string GuidUnidadeAutuadora { get; set; }
      }

    public class ProcessoModelo
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public int IdAtividade { get; set; }
        public string GuidOrganizacaoAutuadora { get; set; }
        public string NomeOrganizacaoAutuadora { get; set; }
        public string SiglaOrganizacaoAutuadora { get; set; }
        public string GuidUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public string DataAutuacao { get; set; }
        public string DataUltimoTramite { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
        public string Numero { get; set; }
    }

    public class ProcessoCompletoModelo
    {
        public int Id { get; set; }
        public string Resumo { get; set; }
        public string GuidOrganizacaoAutuadora { get; set; }
        public string NomeOrganizacaoAutuadora { get; set; }
        public string SiglaOrganizacaoAutuadora { get; set; }
        public string GuidUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public string DataAutuacao { get; set; }
        public string DataUltimoTramite { get; set; }
        public string Numero { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public List<AnexoSimplesModeloGet> Anexos { get; set; }
        public List<DespachoSimplesModeloGet> Despachos { get; set; }
        public List<GetInteressadoPessoaFisicaDto> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaProcessoGetModelo> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloGet> MunicipiosProcesso { get; set; }
        public List<SinalizacaoProcessoGetModelo> Sinalizacoes { get; set; }
        public AtividadeProcessoGetModelo Atividade { get; set; }
    }
}
