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
        // Inventory creates one row per unit, so an unbounded quantity is a DoS vector. Same cap in InventoryService.
        public const int MaxQuantity = 1000;

        public CreateProductRequestValidator(IBrandRepository brandRepository)
        {
            RuleFor(x => x.BrandId)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure. It means if the BrandId is empty, it won't check for existence in the repository. Without this, a list of all failures would be reported.
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
