using Application.Services.Interfaces;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Products.Queries
{
    public class GetProductByIdQuery : IRequest<IResponseWrapper>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler(IProductService productService) : IRequestHandler<GetProductByIdQuery, IResponseWrapper>
    {
        private readonly IProductService _productService = productService;

        public async Task<IResponseWrapper> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(request.ProductId);

            if (product != null)
            {
                return await ResponseWrapper<ProductResponse>.SuccessAsync(product.Adapt<ProductResponse>());
            }

            return await ResponseWrapper<ProductResponse>.FailAsync("Product not found");
        }
    }
}
