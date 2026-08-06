using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Results;
using NttBankMcp.Domain.Results.Common;
using NttBankMcp.Infrastructure.HttpClients;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class CardRepository(
    INttBankApi api,
    ILogger<CardRepository> logger) : ICardRepository
{
    public async Task<IReadOnlyCollection<CardResult>> GetCardsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerCardsAsync(
            customerId, cancellationToken);

        logger.LogCollectionResult(
            nameof(CardRepository),
            nameof(GetCardsByCustomerIdAsync), 
            customerId, 
            result.Count);

        return result;
    }

    public async Task<PagedResult<CardTransactionResult>> GetTransactionsByCardIdAsync(
        int cardId,
        DateTime? from,
        DateTime? toDate,
        int? page,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCardTransactionsAsync(
            cardId, from, toDate, page, pageSize, cancellationToken);

        logger.LogCollectionResult(
            nameof(CardRepository),
            nameof(GetTransactionsByCardIdAsync), 
            cardId, 
            result.Items.Count);

        return result;
    }
}
