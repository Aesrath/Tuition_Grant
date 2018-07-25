using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P13.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<Adummy> Adummy { get; set; }
		public virtual DbSet<HappinessLevel> HappinessLevel { get; set; }
		public virtual DbSet<Member> Member { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<HappinessLevel>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Level)
					.IsRequired()
					.HasColumnType("varchar(50)");
			});

			modelBuilder.Entity<Member>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnType("varchar(50)");

				entity.Property(e => e.Phone).HasColumnType("varchar(8)");

				entity.HasOne(d => d.HappinessLevel)
					.WithMany(p => p.Member)
					.HasForeignKey(d => d.HappinessLevelId)
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_Member_ToHappinessLevel");
			});

			modelBuilder.Entity<Adummy>(entity =>
            {
                entity.ToTable("ADummy");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}