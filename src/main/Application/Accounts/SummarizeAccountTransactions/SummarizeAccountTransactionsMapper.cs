using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public sealed class SummarizeAccountTransactionsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<TransactionGroupSummaryResult, TransactionGroupSummaryResponse>()
            .Map(dest => dest.GroupKey, src => src.GroupKey)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.TransactionCount, src => src.TransactionCount)
            .Map(dest => dest.AverageAmount, src => src.AverageAmount);

        config.NewConfig<TransactionSummaryResult, SummarizeAccountTransactionsResponse>()
            .Map(dest => dest.AccountId, src => src.AccountId)
            .Map(dest => dest.GroupBy, src => src.GroupBy)
            .Map(dest => dest.PeriodFrom, src => src.PeriodFrom)
            .Map(dest => dest.PeriodTo, src => src.PeriodTo)
            .Map(dest => dest.TotalCredits, src => src.TotalCredits)
            .Map(dest => dest.TotalDebits, src => src.TotalDebits)
            .Map(dest => dest.NetFlow, src => src.NetFlow)
            .Map(dest => dest.TotalTransactionsCount, src => src.TotalTransactionsCount)
            .Map(dest => dest.Groups, src => src.Groups);
    }
}
