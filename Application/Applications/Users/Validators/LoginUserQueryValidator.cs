
using FluentValidation;
using Sarafi.Application.Applications.Users.Queries;

namespace Sarafi.Application.Applications.Users.Validators
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(l => l.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username cannot be null or empty!");

            RuleFor(l => l.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password cannot be null or empty!");
        }
    }
}
