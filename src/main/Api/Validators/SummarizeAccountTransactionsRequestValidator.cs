using FluentValidation;
using NttBankMcp.Api.Validators.Common;
using NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

namespace NttBankMcp.Api.Validators;

public sealed class SummarizeAccountTransactionsRequestValidator
    : AbstractValidator<SummarizeAccountTransactionsRequest>
{
    private static readonly string[] AllowedGroupByValues =
    [
        "txn_type", "channel", "merchant_category", "month", "day",
    ];

    public SummarizeAccountTransactionsRequestValidator()
    {
        RuleFor(rule => rule.AccountId)
            .SetValidator(new AccountIdValidator());

        RuleFor(rule => rule.GroupBy)
            .NotEmpty()
            .WithErrorCode("Account.InvalidGroupBy")
            .WithMessage("Group by is required.");

        RuleFor(rule => rule.GroupBy)
            .Must(value => AllowedGroupByValues.Contains(value))
            .WithErrorCode("Account.InvalidGroupBy")
            .WithMessage(
                "Group by must be one of: txn_type, channel, merchant_category, month, day.")
            .When(rule => !string.IsNullOrEmpty(rule.GroupBy));

        RuleFor(rule => rule.ToDate)
            .GreaterThanOrEqualTo(rule => rule.From)
            .WithErrorCode("Account.InvalidPeriod")
            .WithMessage("Period end date must not be earlier than the start date.")
            .When(rule => rule.From.HasValue && rule.ToDate.HasValue);
    }
}
