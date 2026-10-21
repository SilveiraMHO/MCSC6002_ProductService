using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Domain;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Requests.Product;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Products.Commands
{
    public class UpdateProductCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public UpdateProductRequest UpdateProduct { get; set; }
    }

    public class UpdateProductCommandHandler(IProductService productService) : IRequestHandler<UpdateProductCommand, IResponseWrapper>
    {
        private readonly IProductService _productService = productService;

        public async Task<IResponseWrapper> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatedProduct = await _productService.UpdateAsync(request.UpdateProduct.Adapt<Product>());

            if (updatedProduct is not null)
            {
                return await ResponseWrapper<ProductResponse>.SuccessAsync(updatedProduct.Adapt<ProductResponse>(), "Product updated successfully");
            }

            return await ResponseWrapper<ProductResponse>.FailAsync("Product not found");
        }
    }
}
