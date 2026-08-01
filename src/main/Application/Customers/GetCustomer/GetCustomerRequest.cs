using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetCustomer;

public sealed record GetCustomerRequest(
    int CustomerId) : IBaseRequest;
