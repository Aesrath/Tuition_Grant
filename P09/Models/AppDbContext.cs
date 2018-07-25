using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P09.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<MugOrder> MugOrder { get; set; }
        public virtual DbSet<Pokedex> Pokedex { get; set; }
        public virtual DbSet<ShirtOrder> ShirtOrder { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("varchar(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<MugOrder>(entity =>
            {
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedBy).HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Pokedex>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<ShirtOrder>(entity =>
            {
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedBy).HasColumnType("varchar(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });
        }
    }
}