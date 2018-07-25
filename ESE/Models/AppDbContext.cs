using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESE.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<Adummy> Adummy { get; set; }
        public virtual DbSet<Competitor> Competitor { get; set; }
        public virtual DbSet<SkillArea> SkillArea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adummy>(entity =>
            {
                entity.ToTable("ADummy");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Competitor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("varchar(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<SkillArea>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });
        }
    }
}