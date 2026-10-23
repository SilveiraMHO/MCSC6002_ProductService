using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(column => column.Id);

            builder.Property(column => column.Id)
                .HasColumnName("id");

            builder.Property(column => column.BrandId)
                .HasColumnName("brand_id")
                .IsRequired();

            builder.Property(column => column.Name)
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(column => column.Description)
                .HasColumnName("description")
                .HasMaxLength(400);

            builder.Property(column => column.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(column => column.ReOrderThreshold)
                .HasColumnName("reorder_threshold")
                .IsRequired();

            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey(product => product.BrandId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(product => product.Images)
                .WithOne(product => product.Product)
                .HasForeignKey("ProductId") // Shadow FK.
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
