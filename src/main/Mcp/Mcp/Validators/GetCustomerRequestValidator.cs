using FluentValidation;
using NttBank.Mcp.Application.Customers.GetCustomer;

namespace NttBank.Mcp.Mcp.Mcp.Validators;

public sealed class GetCustomerRequestValidator
    : AbstractValidator<GetCustomerRequest>
{
    public GetCustomerRequestValidator()
    {
        RuleFor(rule => rule.CustomerId)
            .GreaterThan(0)
            .WithErrorCode("Customer.InvalidId")
            .WithMessage("Customer identifier must be greater than zero.");
    }
}
