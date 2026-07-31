using JacksonVeroneze.NET.Result;
using NttBank.Mcp.Application.Abstractions.UseCases;

namespace NttBank.Mcp.Application.Customers.GetCustomer;

public interface IGetCustomerUseCase :
    IUseCase<GetCustomerRequest, Result<GetCustomerResponse>>;
