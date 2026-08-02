namespace NttBankMcp.Application.Customers.GetCustomerAccounts;

public sealed record GetCustomerAccountsResponse
{
    public IReadOnlyCollection<CustomerAccountResponse> Accounts { get; init; } = [];
}
