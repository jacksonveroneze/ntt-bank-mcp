using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetCustomer;

public interface IGetCustomerUseCase :
    IUseCase<GetCustomerRequest, Result<GetCustomerResponse>>;
