using Application.Interfaces.Repositories;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> CreateAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
