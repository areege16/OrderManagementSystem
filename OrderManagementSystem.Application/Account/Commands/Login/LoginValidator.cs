using FluentValidation;

namespace OrderManagementSystem.Application.Account.Commands.Login
{
    class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.LoginRequestDto.UserName)
               .NotEmpty()
               .WithMessage("Username is required");

            RuleFor(x => x.LoginRequestDto.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}