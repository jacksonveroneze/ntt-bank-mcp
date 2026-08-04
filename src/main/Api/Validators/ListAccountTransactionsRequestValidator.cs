using FluentValidation;
using NttBankMcp.Application.Accounts.ListAccountTransactions;

namespace NttBankMcp.Api.Validators;

public sealed class ListAccountTransactionsRequestValidator
    : AbstractValidator<ListAccountTransactionsRequest>
{
    public ListAccountTransactionsRequestValidator()
    {
        RuleFor(rule => rule.CustomerId)
            .GreaterThan(0)
            .WithErrorCode("Customer.InvalidId")
            .WithMessage("Customer identifier must be greater than zero.");

        RuleFor(rule => rule.AccountId)
            .GreaterThan(0)
            .WithErrorCode("Account.InvalidId")
            .WithMessage("Account identifier must be greater than zero.");
    }
}
