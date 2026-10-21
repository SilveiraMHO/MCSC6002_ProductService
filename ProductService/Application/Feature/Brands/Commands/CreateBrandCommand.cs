using Application.Pipelines.Interfaces;
using Application.Services.Interfaces;
using Domain;
using MediatR;
using ResponseLibrary.Models.Requests.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Brands.Commands
{
    public class CreateBrandCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public CreateBrandRequest CreateBrand { get; set; }
    }

    public class CreateBrandCommandHandler(IBrandService brandService) : IRequestHandler<CreateBrandCommand, IResponseWrapper>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<IResponseWrapper> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var newBrand = new Brand
            {
                Name = request.CreateBrand.Name,
                Description = request.CreateBrand.Description
            };

            //var newBrand = request.CreateBrand.Adapt<Brand>();

            var brandId = await _brandService.CreateAsync(newBrand);

            return await ResponseWrapper<int>.SuccessAsync(brandId, "Brand created successfully.");
        }
    }
}
