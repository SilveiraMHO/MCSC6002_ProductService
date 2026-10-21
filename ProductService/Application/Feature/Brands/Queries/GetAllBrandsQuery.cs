using Application.Services.Interfaces;
using Mapster;
using MediatR;
using ResponseLibrary.Models.Responses.Product;
using ResponseLibrary.Wrappers;
using ResponseLibrary.Wrappers.Interfaces;

namespace Application.Feature.Brands.Queries
{
    public class GetAllBrandsQuery : IRequest<IResponseWrapper>
    {
    }

    public class GetAllBrandsQueryHandler(IBrandService brandService) : IRequestHandler<GetAllBrandsQuery, IResponseWrapper>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<IResponseWrapper> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandService.GetAllAsync();

            return await ResponseWrapper<List<BrandResponse>>.SuccessAsync(brands.Adapt<List<BrandResponse>>());
        }
    }
}
