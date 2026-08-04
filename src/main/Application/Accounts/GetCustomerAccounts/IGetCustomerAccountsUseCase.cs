using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.GetCustomerAccounts;

public interface IGetCustomerAccountsUseCase :
    IUseCase<GetCustomerAccountsRequest, Result<GetCustomerAccountsResponse>>;
