using FluentValidation;
using NttBankMcp.Application.Customers.GetCustomerAccounts;

namespace NttBankMcp.Api.Validators;

public sealed class GetCustomerAccountsRequestValidator
    : AbstractValidator<GetCustomerAccountsRequest>
{
    public GetCustomerAccountsRequestValidator()
    {
        RuleFor(rule => rule.CustomerId)
            .GreaterThan(0)
            .WithErrorCode("Customer.InvalidId")
            .WithMessage("Customer identifier must be greater than zero.");

        RuleFor(rule => rule.AccountType)
            .IsInEnum()
            .WithErrorCode("Customer.InvalidAccountType")
            .WithMessage("Account type filter must be a known account type.")
            .When(rule => rule.AccountType.HasValue);

        RuleFor(rule => rule.Status)
            .IsInEnum()
            .WithErrorCode("Customer.InvalidAccountStatus")
            .WithMessage("Account status filter must be a known account status.")
            .When(rule => rule.Status.HasValue);
    }
}
