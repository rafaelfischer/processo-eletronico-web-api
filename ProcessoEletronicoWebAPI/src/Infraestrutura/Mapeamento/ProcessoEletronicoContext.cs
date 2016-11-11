using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Infraestrutura.Mapeamento
{
    public partial class ProcessoEletronicoContext : DbContext
    {
        public virtual DbSet<Anexo> Anexo { get; set; }
        public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<DestinacaoFinal> DestinacaoFinal { get; set; }
        public virtual DbSet<DigitoEsfera> DigitoEsfera { get; set; }
        public virtual DbSet<DigitoPoder> DigitoPoder { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Funcao> Funcao { get; set; }
        public virtual DbSet<InteressadoPessoaFisica> InteressadoPessoaFisica { get; set; }
        public virtual DbSet<InteressadoPessoaJuridica> InteressadoPessoaJuridica { get; set; }
        public virtual DbSet<MunicipioProcesso> MunicipioProcesso { get; set; }
        public virtual DbSet<OrganizacaoProcesso> OrganizacaoProcesso { get; set; }
        public virtual DbSet<PlanoClassificacao> PlanoClassificacao { get; set; }
        public virtual DbSet<PrazoGuardaSubjetivo> PrazoGuardaSubjetivo { get; set; }
        public virtual DbSet<Processo> Processo { get; set; }
        public virtual DbSet<Sinalizacao> Sinalizacao { get; set; }
        public virtual DbSet<SinalizacaoProcesso> SinalizacaoProcesso { get; set; }
        public virtual DbSet<TipoContato> TipoContato { get; set; }
        public virtual DbSet<TipoDocumental> TipoDocumental { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=10.32.254.137;Database=ProcessoEletronico;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anexo>(entity =>
            {
                entity.HasIndex(e => new { e.Nome, e.IdProcesso })
                    .HasName("UK_AnexoNomeProcesso")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Conteudo)
                    .IsRequired()
                    .HasColumnName("conteudo");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.IdTipoDocumental).HasColumnName("idTipoDocumental");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdProcessoNavigation)
                    .WithMany(p => p.Anexo)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Anexo_Processo");

                entity.HasOne(d => d.IdTipoDocumentalNavigation)
                    .WithMany(p => p.Anexo)
                    .HasForeignKey(d => d.IdTipoDocumental)
                    .HasConstraintName("FK_Anexo_TipoDocumental");
            });

            modelBuilder.Entity<Atividade>(entity =>
            {
                entity.HasIndex(e => new { e.Codigo, e.IdFuncao })
                    .HasName("UK_AtividadeCodigoFuncao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IdFuncao).HasColumnName("idFuncao");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdFuncaoNavigation)
                    .WithMany(p => p.Atividade)
                    .HasForeignKey(d => d.IdFuncao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Atividade_Funcao");
            });

            modelBuilder.Entity<Contato>(entity =>
            {
                entity.HasIndex(e => new { e.Telefone, e.IdInteressadoPessoaFisica, e.IdInteressadoPessoaJuridica })
                    .HasName("UK_Contato")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdInteressadoPessoaFisica)
                    .IsRequired()
                    .HasColumnName("idInteressadoPessoaFisica");

                entity.Property(e => e.IdInteressadoPessoaJuridica)
                    .IsRequired()
                    .HasColumnName("idInteressadoPessoaJuridica");

                entity.Property(e => e.IdTipoContato).HasColumnName("idTipoContato");

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasColumnName("telefone")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdInteressadoPessoaFisicaNavigation)
                    .WithMany(p => p.Contato)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_InteressadoPessoaFisica");

                entity.HasOne(d => d.IdInteressadoPessoaJuridicaNavigation)
                    .WithMany(p => p.Contato)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_InteressadoPessoaJuridica");

                entity.HasOne(d => d.IdTipoContatoNavigation)
                    .WithMany(p => p.Contato)
                    .HasForeignKey(d => d.IdTipoContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_TipoContato");
            });

            modelBuilder.Entity<DestinacaoFinal>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_DestinacaoFinalDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<DigitoEsfera>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_DigitoEsferaDescricao")
                    .IsUnique();

                entity.HasIndex(e => e.Digito)
                    .HasName("UK_DigitoEsferaDigito")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Digito).HasColumnName("digito");
            });

            modelBuilder.Entity<DigitoPoder>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_DigitoPoderDescricao")
                    .IsUnique();

                entity.HasIndex(e => e.Digito)
                    .HasName("UK_DigitoPoderDigito")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Digito).HasColumnName("digito");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasIndex(e => new { e.Endereco, e.IdInteressadoPessoaFisica, e.IdInteressadoPessoaJuridica })
                    .HasName("UK_Email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasColumnName("endereco")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdInteressadoPessoaFisica)
                    .IsRequired()
                    .HasColumnName("idInteressadoPessoaFisica");

                entity.Property(e => e.IdInteressadoPessoaJuridica)
                    .IsRequired()
                    .HasColumnName("idInteressadoPessoaJuridica");

                entity.HasOne(d => d.IdInteressadoPessoaFisicaNavigation)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Email_InteressadoPessoaFisica");

                entity.HasOne(d => d.IdInteressadoPessoaJuridicaNavigation)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Email_InteressadoPessoaJuridica");
            });

            modelBuilder.Entity<Funcao>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdFuncaoPai).HasColumnName("idFuncaoPai");

                entity.Property(e => e.IdPlanoClassificacao).HasColumnName("idPlanoClassificacao");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdFuncaoPaiNavigation)
                    .WithMany(p => p.InverseIdFuncaoPaiNavigation)
                    .HasForeignKey(d => d.IdFuncaoPai)
                    .HasConstraintName("FK_Funcao_FuncaoPai");

                entity.HasOne(d => d.IdPlanoClassificacaoNavigation)
                    .WithMany(p => p.Funcao)
                    .HasForeignKey(d => d.IdPlanoClassificacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Funcao_PlanoClassificacao");
            });

            modelBuilder.Entity<InteressadoPessoaFisica>(entity =>
            {
                entity.HasIndex(e => new { e.Cpf, e.IdProcesso })
                    .HasName("UK_InteressadoPessoaFisicaCpf")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nome, e.IdProcesso })
                    .HasName("UK_InteressadoPessoaFisicaNome")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("cpf")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.NomeMunicipio)
                    .IsRequired()
                    .HasColumnName("nomeMunicipio")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UfMunicipio)
                    .IsRequired()
                    .HasColumnName("ufMunicipio")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdProcessoNavigation)
                    .WithMany(p => p.InteressadoPessoaFisica)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InteressadoPessoaFisica_Processo");
            });

            modelBuilder.Entity<InteressadoPessoaJuridica>(entity =>
            {
                entity.HasIndex(e => new { e.Cnpj, e.IdProcesso })
                    .HasName("UK_InteressadoPessoaJuridicaCnpj")
                    .IsUnique();

                entity.HasIndex(e => new { e.RazaoSocial, e.IdProcesso })
                    .HasName("UK_InteressadoPessoaJuridicaRazaoSocial")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.NomeMunicipio)
                    .IsRequired()
                    .HasColumnName("nomeMunicipio")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUnidade)
                    .HasColumnName("nomeUnidade")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SiglaUnidade)
                    .HasColumnName("siglaUnidade")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.UfMunicipio)
                    .IsRequired()
                    .HasColumnName("ufMunicipio")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdProcessoNavigation)
                    .WithMany(p => p.InteressadoPessoaJuridica)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_InteressadoPessoaJuridica_Processo");
            });

            modelBuilder.Entity<MunicipioProcesso>(entity =>
            {
                entity.HasIndex(e => new { e.Nome, e.Uf, e.IdProcesso })
                    .HasName("UK_MunicipioProcesso")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.IdProcessoNavigation)
                    .WithMany(p => p.MunicipioProcesso)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MunicipioProcesso_Processo");
            });

            modelBuilder.Entity<OrganizacaoProcesso>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UK_OrganizacaoProcessoCnpj")
                    .IsUnique();

                entity.HasIndex(e => e.IdOrganizacao)
                    .HasName("UK_OrganizacaoProcessoOrganograma")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UK_OrganizacaoProcessoRazaoSocial")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.IdDigitoEsfera).HasColumnName("idDigitoEsfera");

                entity.Property(e => e.IdDigitoPoder).HasColumnName("idDigitoPoder");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasColumnName("nomeFantasia")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NumeroOrganiacao).HasColumnName("numeroOrganiacao");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.IdDigitoEsferaNavigation)
                    .WithMany(p => p.OrganizacaoProcesso)
                    .HasForeignKey(d => d.IdDigitoEsfera)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrganizacaoProcesso_DigitoEsfera");

                entity.HasOne(d => d.IdDigitoPoderNavigation)
                    .WithMany(p => p.OrganizacaoProcesso)
                    .HasForeignKey(d => d.IdDigitoPoder)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrganizacaoProcesso_DigitoPoder");
            });

            modelBuilder.Entity<PlanoClassificacao>(entity =>
            {
                entity.HasIndex(e => new { e.Codigo, e.IdOrganizacao })
                    .HasName("UK_PlanoClassificacaoCodigoOrganizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AreaFim).HasColumnName("areaFim");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.IdOrganizacaoProcessoNavigation)
                    .WithMany(p => p.PlanoClassificacao)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PlanoClassificacao_OrganizacaoProcesso");
            });

            modelBuilder.Entity<PrazoGuardaSubjetivo>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_PrazoGuardaSubjetivoDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<Processo>(entity =>
            {
                entity.HasIndex(e => new { e.Sequencial, e.DigitoPoder, e.DigitoEsfera, e.DigitoOrganizacao, e.Ano })
                    .HasName("UK_ProcessoNumero")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.DataAutuacao)
                    .HasColumnName("dataAutuacao")
                    .HasColumnType("datetime");

                entity.Property(e => e.DigitoEsfera).HasColumnName("digitoEsfera");

                entity.Property(e => e.DigitoOrganizacao).HasColumnName("digitoOrganizacao");

                entity.Property(e => e.DigitoPoder).HasColumnName("digitoPoder");

                entity.Property(e => e.DigitoVerificador).HasColumnName("digitoVerificador");

                entity.Property(e => e.IdAtividade).HasColumnName("idAtividade");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.IdOrgaoAutuador).HasColumnName("idOrgaoAutuador");

                entity.Property(e => e.IdUnidadeAutuadora).HasColumnName("idUnidadeAutuadora");

                entity.Property(e => e.IdUsuarioAutuador)
                    .IsRequired()
                    .HasColumnName("idUsuarioAutuador")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NomeOrgaoAutuador)
                    .IsRequired()
                    .HasColumnName("nomeOrgaoAutuador")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUnidadeAutuadora)
                    .IsRequired()
                    .HasColumnName("nomeUnidadeAutuadora")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUsuarioAutuador)
                    .IsRequired()
                    .HasColumnName("nomeUsuarioAutuador")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Resumo)
                    .IsRequired()
                    .HasColumnName("resumo")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Sequencial).HasColumnName("sequencial");

                entity.Property(e => e.SiglaOrgaoAutuador)
                    .IsRequired()
                    .HasColumnName("siglaOrgaoAutuador")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SiglaUnidadeAutuadora)
                    .IsRequired()
                    .HasColumnName("siglaUnidadeAutuadora")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.IdAtividadeNavigation)
                    .WithMany(p => p.Processo)
                    .HasForeignKey(d => d.IdAtividade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Processo_Atividade");

                entity.HasOne(d => d.IdOrganizacaoProcessoNavigation)
                    .WithMany(p => p.Processo)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Processo_OrganizacaoProcesso");
            });

            modelBuilder.Entity<Sinalizacao>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_Sinalizacao_Descricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cor)
                    .HasColumnName("cor")
                    .HasColumnType("varchar(7)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.Imagem)
                    .HasColumnName("imagem")
                    .HasColumnType("image");

                entity.HasOne(d => d.IdOrganizacaoProcessoNavigation)
                    .WithMany(p => p.Sinalizacao)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Sinalizacao_OrganizacaoProcesso");
            });

            modelBuilder.Entity<SinalizacaoProcesso>(entity =>
            {
                entity.HasIndex(e => new { e.IdProcesso, e.IdSinalizacao })
                    .HasName("UK_SinalizacaoProcesso")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.IdSinalizacao).HasColumnName("idSinalizacao");

                entity.HasOne(d => d.IdProcessoNavigation)
                    .WithMany(p => p.SinalizacaoProcesso)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoProcesso_Processo");

                entity.HasOne(d => d.IdSinalizacaoNavigation)
                    .WithMany(p => p.SinalizacaoProcesso)
                    .HasForeignKey(d => d.IdSinalizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoProcesso_Sinalizacao");
            });

            modelBuilder.Entity<TipoContato>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_TipoContatoDescricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.QuantidadeDigitos).HasColumnName("quantidadeDigitos");
            });

            modelBuilder.Entity<TipoDocumental>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.IdAtividade).HasColumnName("idAtividade");

                entity.Property(e => e.IdDestinacaoFinal).HasColumnName("idDestinacaoFinal");

                entity.Property(e => e.IdPrazoGuardaSubjetivoCorrente).HasColumnName("idPrazoGuardaSubjetivoCorrente");

                entity.Property(e => e.IdPrazoGuardaSubjetivoIntermediaria).HasColumnName("idPrazoGuardaSubjetivoIntermediaria");

                entity.Property(e => e.Obrigatorio).HasColumnName("obrigatorio");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.PrazoGuardaAnosCorrente).HasColumnName("prazoGuardaAnosCorrente");

                entity.Property(e => e.PrazoGuardaAnosIntermediaria).HasColumnName("prazoGuardaAnosIntermediaria");

                entity.HasOne(d => d.IdAtividadeNavigation)
                    .WithMany(p => p.TipoDocumental)
                    .HasForeignKey(d => d.IdAtividade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TipoDocumental_Atividade");

                entity.HasOne(d => d.IdDestinacaoFinalNavigation)
                    .WithMany(p => p.TipoDocumental)
                    .HasForeignKey(d => d.IdDestinacaoFinal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TipoDocumental_DestinacaoFinal");

                entity.HasOne(d => d.IdPrazoGuardaSubjetivoCorrenteNavigation)
                    .WithMany(p => p.TipoDocumentalIdPrazoGuardaSubjetivoCorrenteNavigation)
                    .HasForeignKey(d => d.IdPrazoGuardaSubjetivoCorrente)
                    .HasConstraintName("FK_TipoDocumental_PrazoGuardaSubjetivoCorrente");

                entity.HasOne(d => d.IdPrazoGuardaSubjetivoIntermediariaNavigation)
                    .WithMany(p => p.TipoDocumentalIdPrazoGuardaSubjetivoIntermediariaNavigation)
                    .HasForeignKey(d => d.IdPrazoGuardaSubjetivoIntermediaria)
                    .HasConstraintName("FK_TipoDocumental_PrazoGuardaSubjetivoIntermediaria");
            });
        }
    }
}