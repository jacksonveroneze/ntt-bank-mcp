using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Customers.GetAccount;

public interface IGetAccountUseCase :
    IUseCase<GetAccountRequest, Result<GetAccountResponse>>;
