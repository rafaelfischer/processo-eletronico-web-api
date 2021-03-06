﻿using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public partial class Processo
    {
        public Processo()
        {
            Anexos = new HashSet<Anexo>();
            Bloqueios = new HashSet<Bloqueio>();
            Despachos = new HashSet<Despacho>();
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
        public string NomeOrganizacaoAutuadora { get; set; }
        public string SiglaOrganizacaoAutuadora { get; set; }
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public string IdUsuarioAutuador { get; set; }
        public string NomeUsuarioAutuador { get; set; }
        public DateTime DataAutuacao { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
        public Guid GuidOrganizacaoAutuadora { get; set; }
        public Guid GuidUnidadeAutuadora { get; set; }

        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual ICollection<Bloqueio> Bloqueios { get; set; }
        public virtual ICollection<Despacho> Despachos { get; set; }
        public virtual ICollection<InteressadoPessoaFisica> InteressadosPessoaFisica { get; set; }
        public virtual ICollection<InteressadoPessoaJuridica> InteressadosPessoaJuridica { get; set; }
        public virtual ICollection<MunicipioProcesso> MunicipiosProcesso { get; set; }
        public virtual ICollection<Notificacao> Notificacoes { get; set; }
        public virtual ICollection<SinalizacaoProcesso> SinalizacoesProcesso { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoProcesso { get; set; }
    }
}
