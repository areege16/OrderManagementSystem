using FluentValidation;

namespace OrderManagementSystem.Application.Admin.Products.Commands.CreateProduct
{
    class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CreateProductDto.Name)
              .NotEmpty()
              .WithMessage("Product name is required.")
              .MaximumLength(100)
              .WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.CreateProductDto.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CreateProductDto.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.");
        }
    }
}