using MedApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedApp.Context;

public partial class MedDbContext : DbContext
{
    private readonly string? _connectionString; 
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public MedDbContext(IConfiguration config, DbContextOptions options) : base(options)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(config), "Connection string is not set");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
                entity.ToTable("Medicament");
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(100);
                entity.Property(e => e.Type).HasMaxLength(100);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("Prescription_pk");
                entity.ToTable("Prescription");
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prescription_Patient");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.IdDoctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("PrescriptionMedicament_pk");
                entity.ToTable("PrescriptionMedicament");
                entity.Property(e => e.Dose).HasColumnType("int");
                entity.Property(e => e.Details).HasMaxLength(100);

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(d => d.IdMedicament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrescriptionMedicament_Medicament");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(d => d.IdPrescription)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrescriptionMedicament_Prescription");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
                entity.ToTable("Doctor");
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("Patient_pk");
                entity.ToTable("Patient");
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Birthdate).HasColumnType("datetime");
            });
            OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
