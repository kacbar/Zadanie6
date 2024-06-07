using CW6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CW6.EfConfigurations
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.IdPrescription);
            builder.Property(p => p.IdPrescription).ValueGeneratedOnAdd();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.DueDate).IsRequired();

            builder.HasOne(pre => pre.IdPatientNavigation)
                .WithMany(pat => pat.Prescriptions)
                .HasForeignKey(pre => pre.IdPatient);

            builder.HasOne(p => p.IdDoctorNavigation)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.IdDoctor);
        }
    }
}
