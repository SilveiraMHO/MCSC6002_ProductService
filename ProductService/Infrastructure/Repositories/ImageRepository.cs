using Application.Interfaces.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            //var image = await _context.Images.FindAsync(id);
            var image = await _context.Images.FirstAsync(i => i.Id == id);

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image is not null) return image;

            return new();
        }

        public async Task<List<Image>> GetByProductIdAsync(int productId)
        {
            var images = await _context.Images.Where(i => i.ProductId == productId).ToListAsync();

            if (images.Count > 0) return images;

            return [];
        }
    }
}
