using FluentValidation;

namespace OrderManagementSystem.Application.Admin.Products.Commands.UpdateProductStock
{
    public class UpdateProductStockValidator : AbstractValidator<UpdateProductStockCommand>
    {
        public UpdateProductStockValidator()
        {
            RuleFor(x => x.UpdateProductStockDto.Id)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than zero.");

            RuleFor(x => x.UpdateProductStockDto.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock cannot be negative.")
                .LessThanOrEqualTo(1000000)
                .WithMessage("Stock value is too large.");
        }
    }
}