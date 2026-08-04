using FluentValidation;
using NttBankMcp.Api.Validators.Common;
using NttBankMcp.Application.Customers.GetCustomer;

namespace NttBankMcp.Api.Validators;

public sealed class GetCustomerRequestValidator
    : AbstractValidator<GetCustomerRequest>
{
    public GetCustomerRequestValidator()
    {
        RuleFor(rule => rule.CustomerId)
            .SetValidator(new CustomerIdValidator());
    }
}
