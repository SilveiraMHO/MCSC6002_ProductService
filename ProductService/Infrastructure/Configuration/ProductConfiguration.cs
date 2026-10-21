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

            // Map the Id property explicitly to a column name if desired
            builder.Property(column => column.Id)
                .HasColumnName("id");

            // Explicitly map the BrandId foreign key column if needed
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

            // Configure the Relationship: Product -> Brand (Many-to-One)
            builder.HasOne(product => product.Brand)
                .WithMany() // Leave empty if your Brand entity does not have a List<Product> navigation property
                .HasForeignKey(product => product.BrandId)
                .OnDelete(DeleteBehavior.Restrict) // Or DeleteBehavior.Cascade based on your business logic
                .IsRequired();

            // Configure the Relationship: Product -> Images (One-to-Many)
            builder.HasMany(product => product.Images)
                .WithOne(product => product.Product) // Leave empty if your Image entity does not have a Product navigation property
                .HasForeignKey("ProductId") // Shadow foreign key inside the Image table if Image doesn't have a ProductId property
                .OnDelete(DeleteBehavior.Cascade); // Deleting a product typically deletes its images
        }
    }
}
