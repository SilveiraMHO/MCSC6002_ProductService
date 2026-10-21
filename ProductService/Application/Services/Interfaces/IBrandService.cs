using Domain;

namespace Application.Services.Interfaces
{
    public interface IBrandService
    {
        Task<int> CreateAsync(Brand brand);
        Task<int> DeleteAsync(int id);
        Task<List<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> UpdateAsync(Brand brand);
    }
}
