using Application.Feature.Products.Commands;
using Application.Interfaces.Repositories;
using FluentValidation;
using ResponseLibrary.Models.Requests.Product;

namespace Application.Feature.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IBrandRepository brandRepository, IProductRepository productRepository)
        {
            RuleFor(command => command.UpdateProduct)
                .SetValidator(new UpdateProductRequestValidator(brandRepository, productRepository));
        }
    }

    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator(IBrandRepository brandRepository, IProductRepository productRepository)
        {
            RuleFor(x => x.BrandId)
                .Cascade(CascadeMode.Stop) // Skip the DB lookup when empty.
                .NotEmpty()
                .MustAsync(async (BrandId, ct) => await brandRepository.ExistsAsync(BrandId))
                    .WithMessage("Brand does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Description)
                .MaximumLength(400);
            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, CreateProductRequestValidator.MaxQuantity);
            RuleFor(x => x.ReOrderThreshold)
                .InclusiveBetween(0, CreateProductRequestValidator.MaxQuantity);
        }
    }
}
