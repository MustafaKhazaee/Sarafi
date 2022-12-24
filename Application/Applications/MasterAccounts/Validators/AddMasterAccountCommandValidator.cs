
using FluentValidation;
using Sarafi.Application.Applications.MasterAccounts.Commands;

namespace Sarafi.Application.Applications.MasterAccounts.Validators
{
    public class AddMasterAccountCommandValidator : AbstractValidator<AddMasterAccountCommand>
    {
        public AddMasterAccountCommandValidator()
        {
            RuleFor(ma => ma.MasterAccountName)
                .NotNull()
                .NotEmpty()
                .WithMessage("You must provide a name for master account!");
        }
    }
}
