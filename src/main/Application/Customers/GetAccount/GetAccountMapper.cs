using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Customers.GetAccount;

public sealed class GetAccountMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<AccountResult, GetAccountResponse>()
            .Map(dest => dest.AccountId, src => src.AccountId)
            .Map(dest => dest.BranchId, src => src.BranchId)
            .Map(dest => dest.AccountType, src => src.AccountType)
            .Map(dest => dest.Balance, src => src.Balance)
            .Map(dest => dest.OpenDate, src => src.OpenDate)
            .Map(dest => dest.Status, src => src.Status);
    }
}
