using JacksonVeroneze.NET.Result;
using MapsterMapper;
using NttBank.Mcp.Application.Abstractions.Repositories;
using NttBank.Mcp.Domain.Entities;

namespace NttBank.Mcp.Application.Customers.GetCustomer;

public sealed class GetCustomerUseCase(
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

        var response = mapper
            .Map<Customer, GetCustomerResponse>(customer!);

        return Result<GetCustomerResponse>
            .WithSuccess(response);
    }
}
