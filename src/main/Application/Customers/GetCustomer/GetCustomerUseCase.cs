using JacksonVeroneze.NET.Result;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Extensions;
using NttBankMcp.Domain.Errors;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Customers.GetCustomer;

public sealed class GetCustomerUseCase(
    ILogger<GetCustomerUseCase> logger,
    IMapper mapper,
    ICustomerRepository repository) : IGetCustomerUseCase
{
    public async Task<Result<GetCustomerResponse>> ExecuteAsync(
        GetCustomerRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var customer = await repository.GetByIdAsync(
            request.CustomerId, cancellationToken);

        if (customer is null)
        {
            var error = DomainErrors.CustomerError.NotFound;

            logger.LogNotFound(nameof(GetCustomerUseCase),
                nameof(ExecuteAsync), request.CustomerId);

            return Result<GetCustomerResponse>
                .FromNotFound(error);
        }

        var response = mapper
            .Map<CustomerResult, GetCustomerResponse>(customer);

        return Result<GetCustomerResponse>
            .WithSuccess(response);
    }
}
