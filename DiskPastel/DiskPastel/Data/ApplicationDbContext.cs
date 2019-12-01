using System;
using System.Collections.Generic;
using System.Text;
using DiskPastel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DiskPastel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Destaques> Destaques { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<TipoProduto> TipoProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Destaques>(entity =>
            {
                entity.HasKey(e => e.Iddestaque);

                entity.Property(e => e.Iddestaque).HasColumnName("IDDestaque");

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.Idproduto).HasColumnName("IDProduto");

                entity.HasOne(d => d.IdprodutoNavigation)
                    .WithMany(p => p.Destaques)
                    .HasForeignKey(d => d.Idproduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Destaques_Produto");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Idproduto);

                entity.Property(e => e.Idproduto).HasColumnName("IDProduto");

                entity.Property(e => e.Custo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdtipoProduto).HasColumnName("IDTipoProduto");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Venda)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdtipoProdutoNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdtipoProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Produto_TipoProduto");
            });


            modelBuilder.Entity<TipoProduto>(entity =>
            {
                entity.HasKey(e => e.IdtipoProduto);

                entity.Property(e => e.IdtipoProduto).HasColumnName("IDTipoProduto");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(75);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
