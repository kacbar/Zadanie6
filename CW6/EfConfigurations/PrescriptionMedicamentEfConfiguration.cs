using CW6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CW6.EfConfigurations
{
    public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            builder.HasOne(pm => pm.IdMedicamentNavigation)
                .WithMany(m => m.PrescriptionMedicament)
                .HasForeignKey(pm => pm.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pm => pm.IdPrescriptionNavigation)
                .WithMany(p => p.PrescriptionMedicament)
                .HasForeignKey(pm => pm.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pm => pm.Dose).IsRequired(false);
            builder.Property(pm => pm.Details).IsRequired().HasMaxLength(100);
        }
    }   

}
