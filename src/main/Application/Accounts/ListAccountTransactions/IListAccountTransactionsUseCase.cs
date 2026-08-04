using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public interface IListAccountTransactionsUseCase :
    IUseCase<ListAccountTransactionsRequest, Result<ListAccountTransactionsResponse>>;
