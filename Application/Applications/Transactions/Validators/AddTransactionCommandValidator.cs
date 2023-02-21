
using FluentValidation;
using Sarafi.Application.Applications.Transactions.Commands;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Transactions.Validators;

public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
{
    private readonly IUnitOfWork _unit;
    public AddTransactionCommandValidator(IUnitOfWork unit)
    {

        RuleFor(t => t.Amount)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Amount should be a decimal number greater than zero!");

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
