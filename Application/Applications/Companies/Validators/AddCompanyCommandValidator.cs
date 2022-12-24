
using FluentValidation;
using Sarafi.Application.Applications.Companies.Commands;

namespace Sarafi.Application.Applications.Companies.Validators
{
    public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyCommandValidator()
        {

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("You must provide a name for company name!");
        }
    }
}
