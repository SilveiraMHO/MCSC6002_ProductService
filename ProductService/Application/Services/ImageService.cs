using Application.Interfaces.Repositories;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<int> CreateAsync(Image image)
        {
            var imageId = await _imageRepository.CreateAsync(image);

            return imageId;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var imageId = await _imageRepository.DeleteAsync(id);

            return imageId;
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);

            return image;
        }

        public async Task<List<Image>> GetByProductIdAsync(int productId)
        {
            var images = await _imageRepository.GetByProductIdAsync(productId);

            return images;
        }
    }
}
