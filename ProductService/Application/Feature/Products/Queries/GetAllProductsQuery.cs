using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;

namespace Application.Feature.Products.Queries
{
    /*
    In all classes (commands or queries) that the ICacheable is implemented, the TResponse can not be an Interface (IResponseWrapper) like the other classes.
    It happens because when the CachePipelineBehavior is called, the data can not be serialized and stored as an Interface (an exception occurs), so we need to declare the type that will be stored.
    */
    public class GetAllProductsQuery : IRequest<ResponseWrapper<List<ProductResponse>>>, ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetAllProductsQuery()
        {
            CacheKey = "GetAllProducts";
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
