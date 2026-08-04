using Mapster;
using NttBankMcp.Domain.Results;

namespace NttBankMcp.Application.Accounts.ListCustomerAccounts;

public sealed class ListCustomerAccountsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<IReadOnlyCollection<AccountResult>,
                ListCustomerAccountsResponse>()
            .Map(dest => dest.Accounts, src => src);
    }
}
