using System.Diagnostics.CodeAnalysis;
using System.Net;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Domain.Results;
using NttBankMcp.Infrastructure.HttpClients;
using Refit;

namespace NttBankMcp.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public sealed class BranchRepository(
    INttBankApi api) : IBranchRepository
{
    public async Task<BranchResult?> GetByIdAsync(
        int branchId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await api.GetBranchByIdAsync(
                branchId, cancellationToken);

            return result;
        }
        catch (ApiException ex)
            when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
