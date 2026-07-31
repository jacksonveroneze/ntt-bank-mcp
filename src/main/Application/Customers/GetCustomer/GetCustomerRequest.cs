using NttBank.Mcp.Application.Abstractions.UseCases;

namespace NttBank.Mcp.Application.Customers.GetCustomer;

public sealed record GetCustomerRequest(
    int CustomerId) : IBaseRequest;
