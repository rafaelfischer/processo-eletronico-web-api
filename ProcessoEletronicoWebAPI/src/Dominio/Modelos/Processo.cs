using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Processo
    {
        public Processo()
        {
            Anexo = new HashSet<Anexo>();
            InteressadosPessoaFisica = new HashSet<InteressadoPessoaFisica>();
            InteressadosPessoaJuridica = new HashSet<InteressadoPessoaJuridica>();
            MunicipiosProcesso = new HashSet<MunicipioProcesso>();
            SinalizacoesProcesso = new HashSet<SinalizacaoProcesso>();
        }

        public int Id { get; set; }
        public int Sequencial { get; set; }
        public byte DigitoVerificador { get; set; }
        public byte DigitoPoder { get; set; }
        public byte DigitoEsfera { get; set; }
        public short DigitoOrganizacao { get; set; }
        public short Ano { get; set; }
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
        public DateTime DataAutuacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }

        public virtual ICollection<Anexo> Anexo { get; set; }
        public virtual ICollection<Despacho> Despacho { get; set; }
        public virtual ICollection<InteressadoPessoaFisica> InteressadosPessoaFisica { get; set; }
        public virtual ICollection<InteressadoPessoaJuridica> InteressadosPessoaJuridica { get; set; }
        public virtual ICollection<MunicipioProcesso> MunicipiosProcesso { get; set; }
        public virtual ICollection<SinalizacaoProcesso> SinalizacoesProcesso { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
