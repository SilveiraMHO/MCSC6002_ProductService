using Application.Interfaces.Repositories;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<int> CreateAsync(Brand brand)
        {
            return await _brandRepository.CreateAsync(brand);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _brandRepository.DeleteAsync(id);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            return await _brandRepository.UpdateAsync(brand);
        }
    }
}
