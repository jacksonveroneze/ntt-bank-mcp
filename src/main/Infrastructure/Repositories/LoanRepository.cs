using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;
using NttBankMcp.Infrastructure.HttpClients;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class LoanRepository(
    INttBankApi api,
    ILogger<LoanRepository> logger) : ILoanRepository
{
    public async Task<IReadOnlyCollection<LoanResult>> GetLoansByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerLoansAsync(
            customerId, cancellationToken);

        logger.LogCollectionResult(
            nameof(LoanRepository),
            nameof(GetLoansByCustomerIdAsync), 
            customerId, 
            result.Count);

        return result;
    }

    public async Task<PagedResult<LoanPaymentResult>> GetPaymentsByLoanIdAsync(
        int loanId,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var result = await api.GetLoanPaymentsAsync(
            loanId, page, pageSize, cancellationToken);

        logger.LogCollectionResult(
            nameof(LoanRepository),
            nameof(GetPaymentsByLoanIdAsync), 
            loanId, 
            result.Items.Count);

        return result;
    }
}
