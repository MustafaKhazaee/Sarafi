
using FluentValidation;
using Sarafi.Application.Applications.Accounts.Commands;
using System.Net;

namespace Sarafi.Application.Applications.Accounts.Validators
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(a => a.AccountName)
                .NotNull()
                .NotEmpty()
                .WithMessage("AccountName should be non-empty string with at least 5 characters.");
        }
    }
}
