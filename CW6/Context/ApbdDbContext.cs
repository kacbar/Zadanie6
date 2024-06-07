using Microsoft.EntityFrameworkCore;
using System.Numerics;
using CW6.EfConfigurations;
using CW6.Models;

namespace CW6.Context
{
    public class ApbdDbContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public ApbdDbContext() { }
        public ApbdDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());

            modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());

            modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEfConfiguration());
            
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApbdDbContext).Assembly);
        }
    }
}
