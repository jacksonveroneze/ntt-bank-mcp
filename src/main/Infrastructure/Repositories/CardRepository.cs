using System.Diagnostics.CodeAnalysis;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class CardRepository(
    INttBankApi api) : ICardRepository
{
    public async Task<IReadOnlyCollection<CardResult>> GetCardsByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken)
    {
        var result = await api.GetCustomerCardsAsync(
            customerId, cancellationToken);

        return result;
    }
}
