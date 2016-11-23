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
        public string Resumo { get; set; }
        public int IdOrgaoAutuador { get; set; }
        public string NomeOrgaoAutuador { get; set; }
        public string SiglaOrgaoAutuador { get; set; }
        public int IdUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public DateTime DataAutuacao { get; set; }
        public string Numero
        {
            get
            {
                return Sequencial + "-" + DigitoVerificador + "." + Ano + "." + DigitoPoder + "." + DigitoEsfera + "." + DigitoOrganizacao.ToString().PadLeft(4, '0');
            }
        }

        public List<AnexoModeloNegocio> Anexo { get; set; }
        public List<DespachoModeloNegocio> Despacho { get; set; }
        public List<InteressadoPessoaFisicaModeloNegocio> InteressadoPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModeloNegocio> InteressadoPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloNegocio> MunicipioProcesso { get; set; }
        public List<SinalizacaoModeloNegocio> Sinalizacao { get; set; }
        public AtividadeModeloNegocio Atividade { get; set; }
        public OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}