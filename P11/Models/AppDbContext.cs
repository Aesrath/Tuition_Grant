using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P11.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasIndex(e => new { e.StudentId, e.Semester, e.ModuleId })
                    .HasName("AK_Result_StudentIDSemesterModuleId")
                    .IsUnique();

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.ModuleId)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Result_ToModule");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Result_ToStudent");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}