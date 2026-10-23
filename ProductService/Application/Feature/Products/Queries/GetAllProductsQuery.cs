using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;

namespace Application.Feature.Products.Queries
{
    // Concrete TResponse: the cache can't deserialize an interface.
    public class GetAllProductsQuery : IRequest<ResponseWrapper<List<ProductResponse>>>, ICacheable
    {
        public const string AllProductsCacheKey = "GetAllProducts"; // Cleared by ProductService on writes.

        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetAllProductsQuery()
        {
            CacheKey = AllProductsCacheKey;
        }
    }

    public class GetAllProductsQueryHandler(IProductService productService) : IRequestHandler<GetAllProductsQuery, ResponseWrapper<List<ProductResponse>>>
    {
        private readonly IProductService _productService = productService;

        public async Task<ResponseWrapper<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllAsync();

            if (products.Count > 0)
            {
                return await ResponseWrapper<List<ProductResponse>>.SuccessAsync(products.Adapt<List<ProductResponse>>());
            }

            return await ResponseWrapper<List<ProductResponse>>.FailAsync("Products not found");
        }
    }
}
