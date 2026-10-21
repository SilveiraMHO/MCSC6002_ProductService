using Application.Feature.Brands.Commands;
using Application.Interfaces.Repositories;
using FluentValidation;
using ResponseLibrary.Models.Requests.Product;

namespace Application.Feature.Brands.Validators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator(IBrandRepository brandRepository)
        {
            RuleFor(command => command.UpdateBrand)
                .SetValidator(new UpdateBrandRequestValidator(brandRepository));
        }
    }

    public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
    {
        public UpdateBrandRequestValidator(IBrandRepository serviceBrand)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Description)
                .MaximumLength(400);
        }
    }
}
