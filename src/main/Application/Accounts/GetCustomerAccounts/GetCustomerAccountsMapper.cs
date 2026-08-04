using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.GetCustomerAccounts;

public sealed class GetCustomerAccountsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<IReadOnlyCollection<AccountResult>,
                GetCustomerAccountsResponse>()
            .Map(dest => dest.Accounts, src => src);
    }
}
