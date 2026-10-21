using Application.Interfaces.Messaging;
using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Domain;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Requests.Product;
using ResponseLibrary.Models.Requests.Product.Events;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Products.Commands
{
    public class CreateProductCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public CreateProductRequest CreateProduct { get; set; }
    }

    public class CreateProductCommandHandler(IProductService productService, IEventPublisher eventPublisher) : IRequestHandler<CreateProductCommand, IResponseWrapper>
    {
        private readonly IProductService _productService = productService;
        private readonly IEventPublisher _eventPublisher = eventPublisher;

        public async Task<IResponseWrapper> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productId = await _productService.CreateAsync(request.CreateProduct.Adapt<Product>());

            await _eventPublisher.PublishAsync(@event: new ProductCreatedEvent
            {
                Id = productId,
                Name = request.CreateProduct.Name,
                Quantity = request.CreateProduct.Quantity,
                ReOrderThreshold = request.CreateProduct.ReOrderThreshold
            }, cancellationToken);

            return await ResponseWrapper<int>.SuccessAsync(productId, "Product created successfully");
        }
    }
}
