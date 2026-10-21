using Domain;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(Product product);
        Task<int> DeleteAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> UpdateAsync(Product product);
        Task<bool> ExistsAsync(int id);
    }
}
