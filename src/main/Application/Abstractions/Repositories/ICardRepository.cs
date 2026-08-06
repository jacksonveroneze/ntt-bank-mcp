using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;

namespace NttBankMcp.Application.Abstractions.Repositories;

public interface ICardRepository
{
    Task<IReadOnlyCollection<CardResult>> GetCardsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);

    Task<PagedResult<CardTransactionResult>> GetTransactionsByCardIdAsync(
        int cardId,
        DateTime? from,
        DateTime? toDate,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken);
}
