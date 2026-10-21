using Application.Feature.Brands.Commands;
using FluentValidation;
using ResponseLibrary.Models.Requests.Product;

namespace Application.Feature.Brands.Validators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(command => command.CreateBrand)
                .SetValidator(new CreateBrandRequestValidator());
        }
    }

    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Description)
                .MaximumLength(400);
        }
    }
}
