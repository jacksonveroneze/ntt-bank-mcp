namespace NttBankMcp.Api.Security;

public static class AuthorizationPolicies
{
    public const string JwtAccess = "JwtAccess";
    public const string CustomerRead = "CustomerRead";
    public const string CustomerAccountsRead = "CustomerAccountsRead";
    public const string AccountTransactionsRead = "AccountTransactionsRead";
}
