using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ILoanRepository
{
    Task<IReadOnlyCollection<LoanResult>> GetLoansByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);
}
