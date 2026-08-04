using FluentValidation;
using NttBankMcp.Api.Validators.Common;
using NttBankMcp.Application.Accounts.GetAccount;

namespace NttBankMcp.Api.Validators;

public sealed class GetAccountRequestValidator
    : AbstractValidator<GetAccountRequest>
{
    public GetAccountRequestValidator()
    {
        RuleFor(rule => rule.AccountId)
            .SetValidator(new AccountIdValidator());
    }
}
