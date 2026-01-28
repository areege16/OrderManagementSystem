using FluentValidation;

namespace OrderManagementSystem.Application.Admin.Orders.Commands.UpdateOrderStatus
{
    class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusValidator()
        {
            RuleFor(x => x.UpdateOrderStatusDto.OrderId)
                .GreaterThan(0);

            RuleFor(x => x.UpdateOrderStatusDto.OrderStatus)
                .IsInEnum()
                .WithMessage("Invalid order status");
        }
    }
}