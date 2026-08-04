using FluentValidation;

namespace NttBankMcp.Api.Validators.Common;

public sealed class AccountIdValidator
    : AbstractValidator<int>
{
    public AccountIdValidator()
    {
        RuleFor(rule => rule)
            .GreaterThan(0)
            .WithErrorCode("Account.InvalidId")
            .WithMessage("Account identifier must be greater than zero.");
    }
}
