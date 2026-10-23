using Application.Feature.Products.Commands;
using Application.Interfaces.Repositories;
using FluentValidation;
using ResponseLibrary.Models.Requests.Product;

namespace Application.Feature.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IBrandRepository brandRepository)
        {
            RuleFor(command => command.CreateProduct)
                .SetValidator(new CreateProductRequestValidator(brandRepository));
        }
    }

    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        // One inventory row per unit: unbounded = DoS. Same cap in Inventory.
        public const int MaxQuantity = 1000;

        public CreateProductRequestValidator(IBrandRepository brandRepository)
        {
            RuleFor(x => x.BrandId)
                .Cascade(CascadeMode.Stop) // Skip the DB lookup when empty.
                .NotEmpty()
                .MustAsync(async (brandId, ct) => await brandRepository.ExistsAsync(brandId))
                    .WithMessage("Brand does not exist.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Description)
                .MaximumLength(400);
            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, MaxQuantity);
            RuleFor(x => x.ReOrderThreshold)
                .InclusiveBetween(0, MaxQuantity);
        }
    }
}
