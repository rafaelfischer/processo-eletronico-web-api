using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
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
        public string GuidOrganizacaoAutuadora { get; set; }
        public string NomeOrganizacaoAutuadora { get; set; }
        public string SiglaOrganizacaoAutuadora { get; set; }
        public string GuidUnidadeAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public DateTime DataAutuacao { get; set; }
        public DateTime DataUltimoTramite
        {
            get
            {
                DateTime dataUltimoTramite = DataAutuacao;

                if (Despachos != null)
                {
                    if (Despachos.Count > 0)
                    {
                        var dataUltimoDespacho = Despachos.GroupBy(d => d.IdProcesso)
                                                     .Select(d => d.Max(gd => gd.DataHoraDespacho))
                                                     .SingleOrDefault();
                        if (dataUltimoDespacho != null)
                            dataUltimoTramite = dataUltimoDespacho;
                    }
                }
                else
                    throw new ProcessoEletronicoException("Para obter a data do último tramite é necessário informar os despachos.");

                return dataUltimoTramite;
            }
        }
        public string Numero
        {
            get
            {
                return Sequencial + "-" + DigitoVerificador + "." + Ano + "." + DigitoPoder + "." + DigitoEsfera + "." + DigitoOrganizacao.ToString().PadLeft(4, '0');
            }
        }

        public List<AnexoModeloNegocio> Anexos { get; set; }
        public List<DespachoModeloNegocio> Despachos { get; set; }
        public List<InteressadoPessoaFisicaModeloNegocio> InteressadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridicaModeloNegocio> InteressadosPessoaJuridica { get; set; }
        public List<MunicipioProcessoModeloNegocio> MunicipiosProcesso { get; set; }
        public List<SinalizacaoModeloNegocio> Sinalizacoes { get; set; }
        public AtividadeModeloNegocio Atividade { get; set; }
        public OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}