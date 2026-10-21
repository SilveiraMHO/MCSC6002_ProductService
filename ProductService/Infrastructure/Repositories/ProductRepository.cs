using Application.Interfaces.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var productDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (productDb is not null)
            {
                _context.Products.Remove(productDb);
                await _context.SaveChangesAsync();

                return id;
            }

            return 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var productsDb = await _context.Products.ToListAsync();

            return productsDb;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var productDb = await _context.Products
                .AsNoTracking() // Disconnects the returned entity from EF Core's Change Tracker.
                .FirstOrDefaultAsync(p => p.Id == id);

            return productDb;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (productDb is not null)
            {
                productDb.Name = product.Name;
                productDb.BrandId = product.BrandId;
                productDb.Description = product.Description;
                productDb.Quantity = product.Quantity;
                productDb.ReOrderThreshold = product.ReOrderThreshold;

                _context.Products.Update(productDb);
                await _context.SaveChangesAsync();
            }

            return productDb;
        }
    }
}
