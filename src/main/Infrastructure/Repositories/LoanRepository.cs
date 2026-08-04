using System.Diagnostics.CodeAnalysis;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class LoanRepository(
    INttBankApi api) : ILoanRepository
{
    public async Task<IReadOnlyCollection<LoanResult>> GetLoansByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerLoansAsync(
            customerId, cancellationToken);

        return result;
    }
}
