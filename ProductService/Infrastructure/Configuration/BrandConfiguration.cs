using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("brand");
            builder.HasKey(column => column.Id);

            builder.Property(column => column.Id)
                   .HasColumnName("id");

            builder.Property(column => column.Name)
                   .HasColumnName("name")
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(column => column.Description)
                   .HasColumnName("description")
                   .HasMaxLength(400);
        }
    }
}
