using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;
using NttBankMcp.Application.Accounts.Common;

namespace NttBankMcp.Application.Accounts.GetAccount;

public interface IGetAccountUseCase :
    IUseCase<GetAccountRequest, Result<AccountResponse>>;
