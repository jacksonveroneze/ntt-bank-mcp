using FluentValidation;
using NttBankMcp.Api.Validators.Common;
using NttBankMcp.Application.Accounts.ListAccountTransactions;

namespace NttBankMcp.Api.Validators;

public sealed class ListAccountTransactionsRequestValidator
    : AbstractValidator<ListAccountTransactionsRequest>
{
    public ListAccountTransactionsRequestValidator()
    {
        RuleFor(rule => rule.AccountId)
            .SetValidator(new AccountIdValidator());
    }
}
