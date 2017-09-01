using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronico.Infraestrutura.Mapeamento;

namespace ProcessoEletronicoService.Infraestrutura.Mapeamento
{
    public partial class ProcessoEletronicoContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public virtual DbSet<Anexo> Anexo { get; set; }
        public virtual DbSet<AnexoRascunho> AnexoRascunho { get; set; }
        public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<Atividade> Bloqueio { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<ContatoRascunho> ContatoRascunho { get; set; }
        public virtual DbSet<Despacho> Despacho { get; set; }
        public virtual DbSet<DestinacaoFinal> DestinacaoFinal { get; set; }
        public virtual DbSet<DigitoEsfera> DigitoEsfera { get; set; }
        public virtual DbSet<DigitoPoder> DigitoPoder { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailRascunho> EmailRascunho { get; set; }
        public virtual DbSet<Funcao> Funcao { get; set; }
        public virtual DbSet<InteressadoPessoaFisica> InteressadoPessoaFisica { get; set; }
        public virtual DbSet<InteressadoPessoaFisicaRascunho> InteressadoPessoaFisicaRascunho { get; set; }
        public virtual DbSet<InteressadoPessoaJuridica> InteressadoPessoaJuridica { get; set; }
        public virtual DbSet<InteressadoPessoaJuridicaRascunho> InteressadoPessoaJuridicaRascunho { get; set; }
        public virtual DbSet<MunicipioProcesso> MunicipioProcesso { get; set; }
        public virtual DbSet<MunicipioRascunhoProcesso> MunicipioRascunhoProcesso { get; set; }
        public virtual DbSet<Notificacao> Notificacao { get; set; }
        public virtual DbSet<OrganizacaoProcesso> OrganizacaoProcesso { get; set; }
        public virtual DbSet<PlanoClassificacao> PlanoClassificacao { get; set; }
        public virtual DbSet<Processo> Processo { get; set; }
        public virtual DbSet<RascunhoProcesso> RascunhoProcesso { get; set; }
        public virtual DbSet<Sinalizacao> Sinalizacao { get; set; }
        public virtual DbSet<SinalizacaoProcesso> SinalizacaoProcesso { get; set; }
        public virtual DbSet<SinalizacaoRascunhoProcesso> SinalizacaoRascunhoProcesso { get; set; }
        public virtual DbSet<TipoContato> TipoContato { get; set; }
        public virtual DbSet<TipoDocumental> TipoDocumental { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                optionsBuilder.UseLoggerFactory(new ProcessoEletronicoLoggerFactory());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anexo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Conteudo)
                    .IsRequired()
                    .HasColumnName("conteudo");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.IdDespacho).HasColumnName("idDespacho");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.IdTipoDocumental).HasColumnName("idTipoDocumental");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasColumnName("mimeType")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.Despacho)
                    .WithMany(p => p.Anexos)
                    .HasForeignKey(d => d.IdDespacho)
                    .HasConstraintName("FK_AnexoDespacho");

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.Anexos)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AnexoProcesso");

                entity.HasOne(d => d.TipoDocumental)
                    .WithMany(p => p.Anexos)
                    .HasForeignKey(d => d.IdTipoDocumental)
                    .HasConstraintName("FK_AnexoTipoDocumental");
            });

            modelBuilder.Entity<AnexoRascunho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Conteudo).HasColumnName("conteudo");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.IdRascunhoProcesso).HasColumnName("idRascunhoProcesso");

                entity.Property(e => e.IdTipoDocumental).HasColumnName("idTipoDocumental");

                entity.Property(e => e.MimeType)
                    .HasColumnName("mimeType")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.RascunhoProcesso)
                    .WithMany(p => p.Anexos)
                    .HasForeignKey(d => d.IdRascunhoProcesso)
                    .HasConstraintName("FK_AnexoRascunhoProcesso");

                entity.HasOne(d => d.TipoDocumental)
                    .WithMany(p => p.AnexosRascunho)
                    .HasForeignKey(d => d.IdTipoDocumental)
                    .HasConstraintName("FK_AnexoRascunhoTipoDocumental");
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

                entity.HasOne(d => d.Funcao)
                    .WithMany(p => p.Atividade)
                    .HasForeignKey(d => d.IdFuncao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Atividade_Funcao");
            });

            modelBuilder.Entity<Bloqueio>(entity =>
            {
                entity.Property(e => e.CpfUsuario)
                    .HasColumnName("cpfUsuario")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.DataFim)
                    .HasColumnName("dataFim")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataInicio)
                    .HasColumnName("dataInicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.Motivo)
                    .IsRequired()
                    .HasColumnName("motivo")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.NomeSistema)
                    .IsRequired()
                    .HasColumnName("nomeSistema")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUsuario)
                    .HasColumnName("nomeUsuario")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.Bloqueios)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bloqueio_Processo");
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

                entity.HasOne(d => d.InteressadoPessoaFisica)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_InteressadoPessoaFisica");

                entity.HasOne(d => d.InteressadoPessoaJuridica)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_InteressadoPessoaJuridica");

                entity.HasOne(d => d.TipoContato)
                    .WithMany(p => p.Contatos)
                    .HasForeignKey(d => d.IdTipoContato)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Contato_TipoContato");
            });

            modelBuilder.Entity<ContatoRascunho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdInteressadoPessoaFisicaRascunho).HasColumnName("idInteressadoPessoaFisicaRascunho");

                entity.Property(e => e.IdInteressadoPessoaJuridicaRascunho).HasColumnName("idInteressadoPessoaJuridicaRascunho");

                entity.Property(e => e.IdTipoContato).HasColumnName("idTipoContato");

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.InteressadoPessoaFisicaRascunho)
                    .WithMany(p => p.ContatosRascunho)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisicaRascunho)
                    .HasConstraintName("FK_Contato_InteressadoPessoaFisicaRascunho");

                entity.HasOne(d => d.InteressadoPessoaJuridicaRascunho)
                    .WithMany(p => p.ContatosRascunho)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridicaRascunho)
                    .HasConstraintName("FK_Contato_InteressadoPessoaJuridicaRascunho");

                entity.HasOne(d => d.TipoContato)
                    .WithMany(p => p.ContatosRascunho)
                    .HasForeignKey(d => d.IdTipoContato)
                    .HasConstraintName("FK_ContatoRascunho_TipoContato");
            });

            modelBuilder.Entity<Despacho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataHoraDespacho)
                    .HasColumnName("dataHoraDespacho")
                    .HasColumnType("datetime");

                entity.Property(e => e.GuidOrganizacaoDestino).HasColumnName("guidOrganizacaoDestino");

                entity.Property(e => e.GuidUnidadeDestino).HasColumnName("guidUnidadeDestino");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.IdUsuarioDespachante)
                    .IsRequired()
                    .HasColumnName("idUsuarioDespachante")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NomeOrganizacaoDestino)
                    .IsRequired()
                    .HasColumnName("nomeOrganizacaoDestino")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUnidadeDestino)
                    .IsRequired()
                    .HasColumnName("nomeUnidadeDestino")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUsuarioDespachante)
                    .IsRequired()
                    .HasColumnName("nomeUsuarioDespachante")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.SiglaOrganizacaoDestino)
                    .IsRequired()
                    .HasColumnName("siglaOrganizacaoDestino")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SiglaUnidadeDestino)
                    .IsRequired()
                    .HasColumnName("siglaUnidadeDestino")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .HasColumnType("varchar(max)");

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.Despachos)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Despacho_Processo");
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

                entity.HasOne(d => d.InteressadoPessoaFisica)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Email_InteressadoPessoaFisica");

                entity.HasOne(d => d.InteressadoPessoaJuridica)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Email_InteressadoPessoaJuridica");
            });

            modelBuilder.Entity<EmailRascunho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Endereco)
                    .HasColumnName("endereco")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdInteressadoPessoaFisicaRascunho).HasColumnName("idInteressadoPessoaFisicaRascunho");

                entity.Property(e => e.IdInteressadoPessoaJuridicaRascunho).HasColumnName("idInteressadoPessoaJuridicaRascunho");

                entity.HasOne(d => d.InteressadoPessoaFisicaRascunho)
                    .WithMany(p => p.EmailsRascunho)
                    .HasForeignKey(d => d.IdInteressadoPessoaFisicaRascunho)
                    .HasConstraintName("FK_Email_InteressadoPessoaFisicaRascunho");

                entity.HasOne(d => d.InteressadoPessoaJuridicaRascunho)
                    .WithMany(p => p.EmailsRascunho)
                    .HasForeignKey(d => d.IdInteressadoPessoaJuridicaRascunho)
                    .HasConstraintName("FK_Email_InteressadoPessoaJuridicaRascunho");
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

                entity.HasOne(d => d.FuncaoPai)
                    .WithMany(p => p.FuncoesFilhas)
                    .HasForeignKey(d => d.IdFuncaoPai)
                    .HasConstraintName("FK_Funcao_FuncaoPai");

                entity.HasOne(d => d.PlanoClassificacao)
                    .WithMany(p => p.Funcoes)
                    .HasForeignKey(d => d.IdPlanoClassificacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Funcao_PlanoClassificacao");
            });

            modelBuilder.Entity<InteressadoPessoaFisica>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("cpf")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

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

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.InteressadosPessoaFisica)
                    .HasForeignKey(d => d.IdProcesso)
                    .HasConstraintName("FK_InteressadoPessoaFisica_Processo");
            });

            modelBuilder.Entity<InteressadoPessoaFisicaRascunho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cpf)
                    .HasColumnName("cpf")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

                entity.Property(e => e.IdRascunhoProcesso).HasColumnName("idRascunhoProcesso");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.NomeMunicipio)
                    .HasColumnName("nomeMunicipio")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UfMunicipio)
                    .HasColumnName("ufMunicipio")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.RascunhoProcesso)
                    .WithMany(p => p.InteressadosPessoaFisica)
                    .HasForeignKey(d => d.IdRascunhoProcesso)
                    .HasConstraintName("FK_InteressadoPessoaFisica_RascunhoProcesso");
            });

            modelBuilder.Entity<InteressadoPessoaJuridica>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

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

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.InteressadosPessoaJuridica)
                    .HasForeignKey(d => d.IdProcesso)
                    .HasConstraintName("FK_InteressadoPessoaJuridica_Processo");
            });

            modelBuilder.Entity<InteressadoPessoaJuridicaRascunho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnpj)
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

                entity.Property(e => e.IdRascunhoProcesso).HasColumnName("idRascunhoProcesso");

                entity.Property(e => e.NomeMunicipio)
                    .HasColumnName("nomeMunicipio")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUnidade)
                    .HasColumnName("nomeUnidade")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RazaoSocial)
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SiglaUnidade)
                    .HasColumnName("siglaUnidade")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.UfMunicipio)
                    .HasColumnName("ufMunicipio")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.RascunhoProcesso)
                    .WithMany(p => p.InteressadosPessoaJuridica)
                    .HasForeignKey(d => d.IdRascunhoProcesso)
                    .HasConstraintName("FK_InteressadoPessoaJuridica_RascunhoProcesso");
            });

            modelBuilder.Entity<MunicipioProcesso>(entity =>
            {
                entity.HasIndex(e => new { e.IdProcesso, e.GuidMunicipio })
                    .HasName("UK_MunicipioProcesso")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.MunicipiosProcesso)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MunicipioProcesso_Processo");
            });

            modelBuilder.Entity<MunicipioRascunhoProcesso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GuidMunicipio).HasColumnName("guidMunicipio");

                entity.Property(e => e.IdRascunhoProcesso).HasColumnName("idRascunhoProcesso");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Uf)
                    .IsRequired()
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)");

                entity.HasOne(d => d.RascunhoProcesso)
                    .WithMany(p => p.MunicipiosRascunhoProcesso)
                    .HasForeignKey(d => d.IdRascunhoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_MunicipioRascunhoProcesso_RascunhoProcesso");
            });

            modelBuilder.Entity<Notificacao>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataNotificacao)
                    .HasColumnName("dataNotificacao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdDespacho).HasColumnName("idDespacho");

                entity.Property(e => e.IdProcesso).HasColumnName("idProcesso");

                entity.HasOne(d => d.Despacho)
                    .WithMany(p => p.Notificacoes)
                    .HasForeignKey(d => d.IdDespacho)
                    .HasConstraintName("FK__NotificacaoDespacho");

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.Notificacoes)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__NotificacaoProcesso");
            });

            modelBuilder.Entity<OrganizacaoProcesso>(entity =>
            {
                entity.HasIndex(e => e.Cnpj)
                    .HasName("UK_OrganizacaoProcessoCnpj")
                    .IsUnique();

                entity.HasIndex(e => e.GuidOrganizacao)
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

                entity.Property(e => e.DigitoOrganizacao).HasColumnName("digitoOrganizacao");

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");

                entity.Property(e => e.IdDigitoEsfera).HasColumnName("idDigitoEsfera");

                entity.Property(e => e.IdDigitoPoder).HasColumnName("idDigitoPoder");

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasColumnName("nomeFantasia")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razaoSocial")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.DigitoEsfera)
                    .WithMany(p => p.OrganizacaoProcesso)
                    .HasForeignKey(d => d.IdDigitoEsfera)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrganizacaoProcesso_DigitoEsfera");

                entity.HasOne(d => d.DigitoPoder)
                    .WithMany(p => p.OrganizacaoProcesso)
                    .HasForeignKey(d => d.IdDigitoPoder)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_OrganizacaoProcesso_DigitoPoder");
            });

            modelBuilder.Entity<PlanoClassificacao>(entity =>
            {
                entity.HasIndex(e => new { e.Codigo, e.GuidOrganizacao })
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

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.OrganizacaoProcesso)
                    .WithMany(p => p.PlanosClassificacao)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PlanoClassificacao_OrganizacaoProcesso");
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

                entity.Property(e => e.GuidOrganizacaoAutuadora).HasColumnName("guidOrganizacaoAutuadora");

                entity.Property(e => e.GuidUnidadeAutuadora).HasColumnName("guidUnidadeAutuadora");

                entity.Property(e => e.IdAtividade).HasColumnName("idAtividade");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.IdUsuarioAutuador)
                    .IsRequired()
                    .HasColumnName("idUsuarioAutuador")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NomeOrganizacaoAutuadora)
                    .IsRequired()
                    .HasColumnName("nomeOrganizacaoAutuadora")
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

                entity.Property(e => e.SiglaOrganizacaoAutuadora)
                    .IsRequired()
                    .HasColumnName("siglaOrganizacaoAutuadora")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SiglaUnidadeAutuadora)
                    .IsRequired()
                    .HasColumnName("siglaUnidadeAutuadora")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Atividade)
                    .WithMany(p => p.Processos)
                    .HasForeignKey(d => d.IdAtividade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Processo_Atividade");

                entity.HasOne(d => d.OrganizacaoProcesso)
                    .WithMany(p => p.Processos)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Processo_OrganizacaoProcesso");
            });

            modelBuilder.Entity<RascunhoProcesso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");

                entity.Property(e => e.GuidUnidade).HasColumnName("guidUnidade");

                entity.Property(e => e.IdAtividade).HasColumnName("idAtividade");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.NomeOrganizacao)
                    .IsRequired()
                    .HasColumnName("nomeOrganizacao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NomeUnidade)
                    .IsRequired()
                    .HasColumnName("nomeUnidade")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Resumo)
                    .HasColumnName("resumo")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.SiglaOrganizacao)
                    .IsRequired()
                    .HasColumnName("siglaOrganizacao")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.SiglaUnidade)
                    .IsRequired()
                    .HasColumnName("siglaUnidade")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.Atividade)
                    .WithMany(p => p.RascunhosProcesso)
                    .HasForeignKey(d => d.IdAtividade)
                    .HasConstraintName("FK_RascunhoProcesso_Atividade");

                entity.HasOne(d => d.OrganizacaoRascunhoProcesso)
                    .WithMany(p => p.RascunhosProcesso)
                    .HasForeignKey(d => d.IdOrganizacaoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RascunhoProcesso_OrganizacaoProcesso");
            });

            modelBuilder.Entity<Sinalizacao>(entity =>
            {
                entity.HasIndex(e => e.Descricao)
                    .HasName("UK_Sinalizacao_Descricao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cor)
                    .HasColumnName("cor")
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IdOrganizacaoProcesso).HasColumnName("idOrganizacaoProcesso");

                entity.Property(e => e.Imagem)
                    .HasColumnName("imagem")
                    .HasColumnType("image");

                entity.Property(e => e.MimeType)
                    .HasColumnName("mimeType")
                    .HasColumnType("varchar(200)");
                
                entity.HasOne(d => d.OrganizacaoProcesso)
                    .WithMany(p => p.Sinalizacoes)
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

                entity.HasOne(d => d.Processo)
                    .WithMany(p => p.SinalizacoesProcesso)
                    .HasForeignKey(d => d.IdProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoProcesso_Processo");

                entity.HasOne(d => d.Sinalizacao)
                    .WithMany(p => p.SinalizacaoProcesso)
                    .HasForeignKey(d => d.IdSinalizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoProcesso_Sinalizacao");
            });

            modelBuilder.Entity<SinalizacaoRascunhoProcesso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdRascunhoProcesso).HasColumnName("idRascunhoProcesso");

                entity.Property(e => e.IdSinalizacao).HasColumnName("idSinalizacao");

                entity.HasOne(d => d.RascunhoProcesso)
                    .WithMany(p => p.SinalizacoesRascunhoProcesso)
                    .HasForeignKey(d => d.IdRascunhoProcesso)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoRascunhoProcesso_RascunhoProcesso");

                entity.HasOne(d => d.Sinalizacao)
                    .WithMany(p => p.SinalizacaoRascunhoProcesso)
                    .HasForeignKey(d => d.IdSinalizacao)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SinalizacaoRascunhoProcesso_Sinalizacao");
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

                entity.Property(e => e.Obrigatorio).HasColumnName("obrigatorio");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.PrazoGuardaAnosCorrente).HasColumnName("prazoGuardaAnosCorrente");

                entity.Property(e => e.PrazoGuardaAnosIntermediaria).HasColumnName("prazoGuardaAnosIntermediaria");

                entity.Property(e => e.PrazoGuardaSubjetivoCorrente)
                    .HasColumnName("prazoGuardaSubjetivoCorrente")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.PrazoGuardaSubjetivoIntermediaria)
                    .HasColumnName("prazoGuardaSubjetivoIntermediaria")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.Atividade)
                    .WithMany(p => p.TiposDocumentais)
                    .HasForeignKey(d => d.IdAtividade)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TipoDocumental_Atividade");

                entity.HasOne(d => d.DestinacaoFinal)
                    .WithMany(p => p.TiposDocumentais)
                    .HasForeignKey(d => d.IdDestinacaoFinal)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TipoDocumental_DestinacaoFinal");
            });
        }
        
    }
}
