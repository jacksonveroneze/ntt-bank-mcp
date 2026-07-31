using JacksonVeroneze.NET.Result;

namespace NttBank.Mcp.Domain.Errors;

public static class DomainErrors
{
    public static class CustomerError
    {
        public static Error NotFound =>
            Error.Create("Customer.NotFound",
                "The customer with the specified identifier was not found.");
    }
}
