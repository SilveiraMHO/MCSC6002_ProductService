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
                   .HasMaxLength(100)
                   .IsRequired();

            // URL, path or Base64.
            builder.Property(column => column.Data)
                   .HasColumnName("data")
                   .IsRequired();

        }
    }
}
