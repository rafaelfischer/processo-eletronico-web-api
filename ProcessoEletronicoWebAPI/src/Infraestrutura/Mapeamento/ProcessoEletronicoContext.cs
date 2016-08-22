using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Infraestrutura.Mapeamento
{
    public partial class ProcessoEletronicoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=10.32.254.137;Database=ProcessoEletronico;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assunto>(entity =>
            {
                entity.ToTable("assunto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Orgao>(entity =>
            {
                entity.ToTable("orgao");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasColumnName("sigla")
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Processo>(entity =>
            {
                entity.ToTable("processo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssuntoId).HasColumnName("assunto_id");

                entity.Property(e => e.DataAutuacao)
                    .HasColumnName("data_autuacao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Digito).HasColumnName("digito");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.OrgaoAutuacaoId).HasColumnName("orgao_autuacao_id");

                entity.Property(e => e.Resumo)
                    .IsRequired()
                    .HasColumnName("resumo")
                    .HasColumnType("varchar(500)");

                entity.HasOne(d => d.Assunto)
                    .WithMany(p => p.Processo)
                    .HasForeignKey(d => d.AssuntoId)
                    .HasConstraintName("fk_processo_assunto");

                entity.HasOne(d => d.OrgaoAutuacao)
                    .WithMany(p => p.Processo)
                    .HasForeignKey(d => d.OrgaoAutuacaoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_processo_orgao");
            });

            modelBuilder.Entity<TipoDocumental>(entity =>
            {
                entity.ToTable("TipoDocumental");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasColumnType("varchar(100)");
            });
        }

        public virtual DbSet<Assunto> Assunto { get; set; }
        public virtual DbSet<Orgao> Orgao { get; set; }
        public virtual DbSet<Processo> Processo { get; set; }
    }
}