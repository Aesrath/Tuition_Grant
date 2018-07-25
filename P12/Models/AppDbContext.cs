using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P12.Models
{
    public partial class AppDbContext : DbContext
    {
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.PatientId)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Patient_ToSchedule");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("varchar(10)");

                entity.Property(e => e.Icpassport)
                    .IsRequired()
                    .HasColumnName("ICPassport")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.DoctorId)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Weekday).HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Staff>(entity =>
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