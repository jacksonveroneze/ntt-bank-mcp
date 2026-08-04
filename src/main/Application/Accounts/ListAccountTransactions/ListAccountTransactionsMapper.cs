using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.ListAccountTransactions;

public sealed class ListAccountTransactionsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<AccountTransactionResult, AccountTransactionResponse>()
            .Map(dest => dest.TransactionId, src => src.TransactionId)
            .Map(dest => dest.AccountId, src => src.AccountId)
            .Map(dest => dest.TransactionDate, src => src.TransactionDate)
            .Map(dest => dest.TransactionType, src => src.TransactionType)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Channel, src => src.Channel)
            .Map(dest => dest.MerchantCategory, src => src.MerchantCategory);

        config.NewConfig<IReadOnlyCollection<AccountTransactionResult>,
                ListAccountTransactionsResponse>()
            .Map(dest => dest.Transactions, src => src);
    }
}
