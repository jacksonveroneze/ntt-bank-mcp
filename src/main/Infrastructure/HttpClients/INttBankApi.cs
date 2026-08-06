using NttBankMcp.Domain.Enums;
using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;
using Refit;

namespace NttBankMcp.Infrastructure.HttpClients;

public interface INttBankApi
{
    #region Customer

    [Get("/v1/customers")]
    Task<PagedResult<CustomerResult>> GetCustomersAsync(
        [Query("page")] int? page,
        [Query("pageSize")] int? pageSize,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}")]
    Task<CustomerResult> GetCustomerByIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/relationships")]
    Task<IReadOnlyCollection<RelationshipResult>> GetCustomerRelationshipsAsync(
        int customerId,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/accounts")]
    Task<IReadOnlyCollection<AccountResult>> GetCustomerAccountsAsync(
        int customerId,
        [Query("accountType")] AccountType? accountType,
        [Query("status")] AccountStatus? status,
        [Query("hasBalance")] bool? hasBalance,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/cards")]
    Task<IReadOnlyCollection<CardResult>> GetCustomerCardsAsync(
        int customerId,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/loans")]
    Task<IReadOnlyCollection<LoanResult>> GetCustomerLoansAsync(
        int customerId,
        CancellationToken cancellationToken);

    [Get("/v1/customers/{customerId}/tickets")]
    Task<IReadOnlyCollection<TicketResult>> GetCustomerTicketsAsync(
        int customerId,
        [Query("status")] string? status,
        [Query("type")] string? type,
        CancellationToken cancellationToken);

    #endregion

    #region Account

    [Get("/v1/accounts/{accountId}")]
    Task<AccountResult> GetAccountByIdAsync(
        int accountId,
        CancellationToken cancellationToken);

    [Get("/v1/accounts/{accountId}/transactions")]
    Task<IReadOnlyCollection<AccountTransactionResult>> GetTransactionsByAccountIdAsync(
        int accountId,
        CancellationToken cancellationToken);

    [Get("/v1/accounts/{accountId}/transactions/summary")]
    Task<TransactionSummaryResult> SummarizeAccountTransactionsAsync(
        int accountId,
        [Query("groupBy")] string groupBy,
        [Query("from")] DateTime? from,
        [Query("to")] DateTime? toDate,
        CancellationToken cancellationToken);

    #endregion

    #region Card

    [Get("/v1/cards/{cardId}/transactions")]
    Task<PagedResult<CardTransactionResult>> GetCardTransactionsAsync(
        int cardId,
        [Query("from")] DateTime? from,
        [Query("to")] DateTime? toDate,
        [Query("page")] int? page,
        [Query("pageSize")] int? pageSize,
        CancellationToken cancellationToken);

    #endregion

    #region Loan

    [Get("/v1/loans/{loanId}/payments")]
    Task<PagedResult<LoanPaymentResult>> GetLoanPaymentsAsync(
        int loanId,
        [Query("page")] int? page,
        [Query("pageSize")] int? pageSize,
        CancellationToken cancellationToken);

    #endregion

    #region Branch

    [Get("/v1/branches/{branchId}")]
    Task<BranchResult> GetBranchByIdAsync(
        int branchId,
        CancellationToken cancellationToken);

    [Get("/v1/branches/{branchId}/employees")]
    Task<IReadOnlyCollection<EmployeeResult>> GetBranchEmployeesAsync(
        int branchId,
        CancellationToken cancellationToken);

    #endregion
}
