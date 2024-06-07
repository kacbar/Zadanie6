using CW6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CW6.EfConfigurations
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(m => m.IdMedicament);
            builder.Property(m => m.IdMedicament).ValueGeneratedOnAdd();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Type).IsRequired().HasMaxLength(100);
        }
    }
}
