using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;
using Refit;

namespace NttBankMcp.Infrastructure.HttpClients;

public interface INttBankApi
{
    #region Customer

    [Get("/v1/customers/{customerId}")]
    Task<CustomerResult> GetCustomerByIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/accounts")]
    Task<IReadOnlyCollection<AccountResult>> GetCustomerAccountsAsync(
        int customerId,
        [Query("accountType")] AccountType? accountType,
        [Query("status")] AccountStatus? status,
        [Query("hasBalance")] bool? hasBalance,
        CancellationToken cancellationToken);

    #endregion

    #region Branch

    [Get("/v1/branches/{branchId}")]
    Task<BranchResult> GetBranchByIdAsync(
        int branchId,
        CancellationToken cancellationToken);

    #endregion
}
