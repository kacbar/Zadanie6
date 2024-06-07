using CW6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW6.EfConfigurations
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.IdDoctor);
            builder.Property(d => d.IdDoctor).ValueGeneratedOnAdd();
            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Email).HasMaxLength(100);
        }
    }
}