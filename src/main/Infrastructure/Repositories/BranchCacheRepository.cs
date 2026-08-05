using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Hybrid;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class BranchCacheRepository(
    HybridCache cache,
    IBranchRepository repository) : IBranchCacheRepository
{
    public async Task<BranchResult?> GetByIdAsync(
        int branchId, CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync(
            $"branch:{branchId}",
            (branchId, obj: this),
            static async (state, token) =>
                await state.obj.GetDataFromTheSourceAsync(state.branchId, token),
            cancellationToken: cancellationToken
        );
    }

    private Task<BranchResult?> GetDataFromTheSourceAsync(
        int branchId, CancellationToken token)
    {
        return repository.GetByIdAsync(branchId, token);
    }
}
