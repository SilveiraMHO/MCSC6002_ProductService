using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Domain;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Requests.Product;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Brands.Commands
{
    public class UpdateBrandCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public UpdateBrandRequest UpdateBrand { get; set; }
    }

    public class UpdateBrandCommandHandler(IBrandService brandService) : IRequestHandler<UpdateBrandCommand, IResponseWrapper>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<IResponseWrapper> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand
            {
                Id = request.UpdateBrand.Id,
                Name = request.UpdateBrand.Name,
                Description = request.UpdateBrand.Description
            };

            var updatedBrand = await _brandService.UpdateAsync(brand);

            if (updatedBrand is not null)
            {
                return await ResponseWrapper<BrandResponse>.SuccessAsync(updatedBrand.Adapt<BrandResponse>(), "Brand updated successfully.");
            }

            return await ResponseWrapper<BrandResponse>.FailAsync("Product not found");
        }
    }
}
