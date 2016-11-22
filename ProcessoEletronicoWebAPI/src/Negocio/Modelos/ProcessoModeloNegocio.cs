using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class ProcessoModeloNegocio
    {
        public int Id { get; set; }
        public int Sequencial { get; set; }
        public byte DigitoVerificador { get; set; }
        public byte DigitoPoder { get; set; }
        public byte DigitoEsfera { get; set; }
        public short DigitoOrganizacao { get; set; }
        public short Ano { get; set; }
        public AtividadeModeloNegocio Atividade { get; set; }
        public string Resumo { get; set; }
        public List<InteressadoPessoaFisicaModeloNegocio> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModeloNegocio> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloNegocio> Municipios { get; set; }
        public List<AnexoModeloNegocio> Anexos { get; set; }
        public List<SinalizacaoModeloNegocio> Sinalizacoes { get; set; }
        public OrganizacaoProcessoModeloNegocio Organizacao { get; set; }
        public int IdOrgaoAutuador { get; set; }
        public string OrgaoAutuador { get; set; }
        public string SiglaOrgaoAutuador { get; set; }
        public int IdUnidadeAutuadora { get; set; }
        public string UnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string UsuarioAutuador { get; set; }
        public DateTime DataAutuacao { get; set; }
    }
}