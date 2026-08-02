using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public interface IGetCustomerAccountsUseCase :
    IUseCase<GetCustomerAccountsRequest, Result<GetCustomerAccountsResponse>>;
