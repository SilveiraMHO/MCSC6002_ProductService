using Domain;

namespace Application.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<int> CreateAsync(Brand brand);
        Task<int> DeleteAsync(int id);
        Task<List<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> UpdateAsync(Brand brand);
        Task<bool> ExistsAsync(int id);
    }
}
