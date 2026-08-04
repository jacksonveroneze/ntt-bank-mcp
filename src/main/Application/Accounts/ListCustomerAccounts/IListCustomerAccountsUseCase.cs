using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.ListCustomerAccounts;

public interface IListCustomerAccountsUseCase :
    IUseCase<ListCustomerAccountsRequest, Result<ListCustomerAccountsResponse>>;
