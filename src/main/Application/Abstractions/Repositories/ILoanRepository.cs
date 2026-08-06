using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ILoanRepository
{
    Task<IReadOnlyCollection<LoanResult>> GetLoansByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<PagedResult<LoanPaymentResult>> GetPaymentsByLoanIdAsync(
        int loanId,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken);
}
