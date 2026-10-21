using Domain;

namespace Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<int> CreateAsync(Image image);
        Task<int> DeleteAsync(int id);
        Task<Image> GetByIdAsync(int id);
        Task<List<Image>> GetByProductIdAsync(int productId);
    }
}
