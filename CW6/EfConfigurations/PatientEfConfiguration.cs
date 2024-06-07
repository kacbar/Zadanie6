using CW6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CW6.EfConfigurations
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.IdPatient);
            builder.Property(p => p.IdPatient).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Birthdate).IsRequired();
        }
    }
}
