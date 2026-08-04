using FluentValidation;
using NttBankMcp.Application.Customers.GetAccount;

namespace NttBankMcp.Api.Validators;

public sealed class GetAccountRequestValidator
    : AbstractValidator<GetAccountRequest>
{
    public GetAccountRequestValidator()
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
