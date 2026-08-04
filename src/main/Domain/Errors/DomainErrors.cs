using JacksonVeroneze.NET.Result;

namespace NttBankMcp.Domain.Errors;

public static class DomainErrors
{
    public static class CustomerError
    {
        public static Error NotFound =>
            Error.Create("Customer.NotFound",
                "The customer with the specified identifier was not found.");
    }

    public static class AccountError
    {
        public static Error NotFound =>
            Error.Create("Account.NotFound",
                "The account with the specified identifier was not found.");
    }
}
