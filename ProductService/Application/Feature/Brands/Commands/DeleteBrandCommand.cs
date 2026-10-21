using Application.Services.Interfaces;
using MediatR;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<IResponseWrapper>
    {
        public int Id { get; set; }
    }

    public class DeleteBrandCommandHandler(IBrandService brandService) : IRequestHandler<DeleteBrandCommand, IResponseWrapper>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<IResponseWrapper> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brandId = await _brandService.DeleteAsync(request.Id);

            if (brandId > 0)
            {
                return await ResponseWrapper.SuccessAsync("Brand deleted successfully");
            }

            return await ResponseWrapper.SuccessAsync("Brand not found");
        }
    }
}
