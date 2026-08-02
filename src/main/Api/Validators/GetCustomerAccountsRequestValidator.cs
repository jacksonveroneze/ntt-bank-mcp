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
    }
}
