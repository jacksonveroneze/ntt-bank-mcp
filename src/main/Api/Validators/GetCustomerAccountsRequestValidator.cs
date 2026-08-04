using FluentValidation;
using NttBankMcp.Api.Validators.Common;
using NttBankMcp.Application.Accounts.GetCustomerAccounts;

namespace NttBankMcp.Api.Validators;

public sealed class GetCustomerAccountsRequestValidator
    : AbstractValidator<GetCustomerAccountsRequest>
{
    public GetCustomerAccountsRequestValidator()
    {
        RuleFor(rule => rule.CustomerId)
            .SetValidator(new CustomerIdValidator());

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
