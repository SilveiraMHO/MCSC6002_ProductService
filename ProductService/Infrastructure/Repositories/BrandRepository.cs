using Application.Interfaces.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return brand.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var brandDb = await _context.Brands.FirstAsync(b => b.Id == id);

            if (brandDb is not null)
            {
                _context.Brands.Remove(brandDb);
                await _context.SaveChangesAsync();

                return id;
            }

            return 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Brands.AnyAsync(b => b.Id == id);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            var BrandsDb = await _context.Brands.ToListAsync();

            return BrandsDb;
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            var brandDb = await _context.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return brandDb;
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            var brandDb = await _context.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);

            if (brandDb is not null)
            {
                brandDb.Name = brand.Name;
                brandDb.Description = brand.Description;

                _context.Brands.Update(brandDb);
                await _context.SaveChangesAsync();
            }

            return brandDb;
        }
    }
}
