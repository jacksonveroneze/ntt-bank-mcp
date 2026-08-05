using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Hybrid;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.Parameters;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class BranchCacheRepository(
    HybridCache cache,
    IBranchRepository repository,
    BranchCacheConfiguration branchCacheConfiguration) : IBranchCacheRepository
{
    public async Task<BranchResult?> GetByIdAsync(
        int branchId, CancellationToken cancellationToken)
    {
        var entryOptions = new HybridCacheEntryOptions
        {
            Expiration = branchCacheConfiguration.Expiration,
            LocalCacheExpiration = branchCacheConfiguration.Expiration,
        };

        return await cache.GetOrCreateAsync(
            $"{branchCacheConfiguration.PrefixKey}:{branchId}",
            (branchId, obj: this),
            static async (state, token) =>
                await state.obj.GetDataFromSourceAsync(state.branchId, token),
            entryOptions,
            cancellationToken: cancellationToken
        );
    }

    private Task<BranchResult?> GetDataFromSourceAsync(
        int branchId, CancellationToken token)
    {
        return repository.GetByIdAsync(branchId, token);
    }
}
