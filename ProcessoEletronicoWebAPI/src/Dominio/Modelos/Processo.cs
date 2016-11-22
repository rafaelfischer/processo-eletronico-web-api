using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Processo
    {
        public Processo()
        {
            Anexo = new HashSet<Anexo>();
            InteressadoPessoaFisica = new HashSet<InteressadoPessoaFisica>();
            InteressadoPessoaJuridica = new HashSet<InteressadoPessoaJuridica>();
            MunicipioProcesso = new HashSet<MunicipioProcesso>();
            SinalizacaoProcesso = new HashSet<SinalizacaoProcesso>();
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
        public virtual ICollection<InteressadoPessoaFisica> InteressadoPessoaFisica { get; set; }
        public virtual ICollection<InteressadoPessoaJuridica> InteressadoPessoaJuridica { get; set; }
        public virtual ICollection<MunicipioProcesso> MunicipioProcesso { get; set; }
        public virtual ICollection<SinalizacaoProcesso> SinalizacaoProcesso { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
