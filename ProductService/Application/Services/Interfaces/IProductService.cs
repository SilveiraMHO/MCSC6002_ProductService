using Domain;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateAsync(Product product);
        Task<int> DeleteAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> UpdateAsync(Product product);
    }
}
