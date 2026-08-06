using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface IBranchRepository
{
    Task<BranchResult?> GetByIdAsync(
        int branchId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<EmployeeResult>> GetEmployeesByBranchIdAsync(
        int branchId,
        CancellationToken cancellationToken);
}
