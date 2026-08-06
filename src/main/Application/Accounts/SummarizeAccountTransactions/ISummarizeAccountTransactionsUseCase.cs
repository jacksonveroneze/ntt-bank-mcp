using JacksonVeroneze.NET.Result;
using NttBankMcp.Application.Abstractions.UseCases;

namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public interface ISummarizeAccountTransactionsUseCase :
    IUseCase<SummarizeAccountTransactionsRequest, Result<SummarizeAccountTransactionsResponse>>;
