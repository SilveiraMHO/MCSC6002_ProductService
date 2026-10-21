using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("image");
            builder.HasKey(column => column.Id);

            builder.Property(column => column.Id)
                   .HasColumnName("id");

            builder.Property(column => column.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();

            builder.Property(column => column.Name)
                   .HasColumnName("name")
                   .HasMaxLength(100) // Adjust length to fit typical file names
                   .IsRequired();

            // Data usually stores a URL, file path, or Base64 string
            builder.Property(column => column.Data)
                   .HasColumnName("data")
                   .IsRequired();

            // Note: The relationship to Product is already handled in ProductConfiguration.
            // There is no need to duplicate builder.HasOne(i => i.Product) here.
        }
    }
}
