
using FluentValidation;
using Sarafi.Application.Applications.Transactions.Commands;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Transactions.Validators
{
    public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
    {
        public AddTransactionCommandValidator()
        {
            RuleFor(t => t.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Amount should be a decimal number greater than 0!");

            RuleFor(t => t.Commission)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Commission should be a decimal number greater than or equal to 0!");

            RuleFor(t => t).Must(t =>
            {
                if (
                       (t.FromAccountId == null && t.TransactionType != TransactionType.Deposit) ||
                       (t.ToAccountId == null && t.TransactionType != TransactionType.Withdraw) ||
                       ((t.FromAccountId == null || t.ToAccountId == null) && t.TransactionType != TransactionType.Transfer) ||
                       (t.FromAccountId == t.ToAccountId)
                   )
                    return false;
                return true;
            }).WithMessage($"Not a valid transaction!");
        }
    }
}
