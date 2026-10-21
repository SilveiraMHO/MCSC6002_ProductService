using Application.Interfaces.Messaging;
using Application.Services.Interfaces;
using MediatR;
using ResponseLibrary.Models.Requests.Product.Events;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Products.Commands
{
    public class DeleteProductCommand : IRequest<IResponseWrapper>
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler(IProductService productService, IEventPublisher eventPublisher) : IRequestHandler<DeleteProductCommand, IResponseWrapper>
    {
        private readonly IProductService _productService = productService;
        private readonly IEventPublisher _eventPublisher = eventPublisher;

        public async Task<IResponseWrapper> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productId = await _productService.DeleteAsync(request.ProductId);

            if (productId > 0)
            {
                await _eventPublisher.PublishAsync(@event: new ProductDeletedEvent
                {
                    Id = productId
                }, cancellationToken);

                return await ResponseWrapper.SuccessAsync("Product deleted successfully");
            }

            return await ResponseWrapper.SuccessAsync("Product not found");
        }
    }
}
