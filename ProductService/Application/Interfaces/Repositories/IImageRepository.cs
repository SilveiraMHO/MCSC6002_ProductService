using Domain;

namespace Application.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<int> CreateAsync(Image image);
        Task<int> DeleteAsync(int id);
        Task<Image> GetByIdAsync(int id);
        Task<List<Image>> GetByProductIdAsync(int productId);
    }
}
