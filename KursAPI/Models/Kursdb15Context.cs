using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KursAPI.Models
{

    public partial class Kursdb15Context : DbContext
    {
        public Kursdb15Context()
        {
            Database.EnsureCreated();
        }

        public Kursdb15Context(DbContextOptions<Kursdb15Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<AbonimentnieKartochki> AbonimentnieKartochkis { get; set; }

        public virtual DbSet<Chitateli> Chitatelis { get; set; }

        public virtual DbSet<KatalogKnig> KatalogKnigs { get; set; }

        public virtual DbSet<VidachaKnig> VidachaKnigs { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Admin");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(true);
                entity.Property(e => e.Password)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AbonimentnieKartochki>(entity =>
            {
                entity.HasKey(e => e.ChitatelId);

                entity.ToTable("AbonimentnieKartochki");

                entity.Property(e => e.ChitatelId).ValueGeneratedNever();

                entity.HasOne(d => d.Chitatel).WithOne(p => p.AbonimentnieKartochki)
                    .HasForeignKey<AbonimentnieKartochki>(d => d.ChitatelId)
                    .HasConstraintName("FK_AbonimentnieKartochki_Chitateli");
            });

            modelBuilder.Entity<Chitateli>(entity =>
            {
                entity.HasKey(e => e.ChitatelId);

                entity.ToTable("Chitateli");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KatalogKnig>(entity =>
            {
                entity.HasKey(e => e.IdKniga);

                entity.ToTable("KatalogKnig");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Genre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VidachaKnig>(entity =>
            {
                entity.ToTable("VidachaKnig");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ChitatelId).HasColumnName("chitatelId");
                entity.Property(e => e.DataVidachi).HasColumnName("dataVidachi");
                entity.Property(e => e.DataVozvrata).HasColumnName("dataVozvrata");
                entity.Property(e => e.IdKniga).HasColumnName("idKniga");

                entity.HasOne(d => d.Chitatel).WithMany(p => p.VidachaKnigs)
                    .HasForeignKey(d => d.ChitatelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VidachaKnig_Chitateli");

                entity.HasOne(d => d.IdKnigaNavigation).WithMany(p => p.VidachaKnigs)
                    .HasForeignKey(d => d.IdKniga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VidachaKnig_KatalogKnig");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
