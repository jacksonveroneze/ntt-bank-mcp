using FluentValidation;

namespace NttBankMcp.Api.Validators.Common;

public sealed class CustomerIdValidator
    : AbstractValidator<int>
{
    public CustomerIdValidator()
    {
        RuleFor(rule => rule)
            .GreaterThan(0)
            .WithErrorCode("Customer.InvalidId")
            .WithMessage("Customer identifier must be greater than zero.");
    }
}
