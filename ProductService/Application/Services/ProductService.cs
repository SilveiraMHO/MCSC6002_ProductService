using Application.Feature.Products.Queries;
using Application.Interfaces.Repositories;
using Application.Services.Interfaces;
using Application.Settings;
using Domain;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;
        private readonly string _allProductsCacheKey;

        public ProductService(IProductRepository productRepository, IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _productRepository = productRepository;
            _cache = cache;
            _allProductsCacheKey = $"{cacheSettings.Value.ApplicationName}:{GetAllProductsQuery.AllProductsCacheKey}"; // Same format as CachePipelineBehavior.
        }

        public async Task<int> CreateAsync(Product product)
        {
            var id = await _productRepository.CreateAsync(product);
            await _cache.RemoveAsync(_allProductsCacheKey); // The cached product list is now stale.
            return id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            await _cache.RemoveAsync(_allProductsCacheKey);
            return result;
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
            var updated = await _productRepository.UpdateAsync(product);
            await _cache.RemoveAsync(_allProductsCacheKey);
            return updated;
        }
    }
}
