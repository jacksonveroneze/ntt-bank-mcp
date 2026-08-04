using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.GetAccount;

public sealed class GetAccountMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<AccountResult, GetAccountResponse>()
            .Map(dest => dest.Account, src => src);
    }
}
