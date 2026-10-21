using Application.Services.Interfaces;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Brands.Queries
{
    public class GetBrandByIdQuery : IRequest<IResponseWrapper>
    {
        public int Id { get; set; }
    }

    public class GetBrandByIdQueryHandler(IBrandService brandService) : IRequestHandler<GetBrandByIdQuery, IResponseWrapper>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<IResponseWrapper> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandService.GetByIdAsync(request.Id);

            if (brand != null)
            {
                return await ResponseWrapper<BrandResponse>.SuccessAsync(brand.Adapt<BrandResponse>());
            }

            return await ResponseWrapper<BrandResponse>.FailAsync("Brand not found");
        }
    }
}
