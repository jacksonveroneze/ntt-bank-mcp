using System.ComponentModel;

namespace NttBank.Mcp.Application.Customers.GetCustomer;

public sealed record GetCustomerResponse
{
    [Description("The unique identifier for the customer")]
    public int CustomerId { get; init; }

    [Description("The name of the customer")]
    public string? Name { get; init; }

    [Description("The gender of the customer")]
    public string? Gender { get; init; }

    [Description("The date of birth of the customer")]
    public string? DateOfBirth { get; init; }

    [Description("The city where the customer resides")]
    public string? City { get; init; }

    [Description("The state where the customer resides")]
    public string? State { get; init; }

    [Description("The phone number of the customer")]
    public string? Phone { get; init; }

    [Description("The email address of the customer")]
    public string? Email { get; init; }

    [Description("The occupation of the customer")]
    public string? Occupation { get; init; }

    [Description("The annual income of the customer")]
    public decimal AnnualIncome { get; init; }

    [Description("The date the customer joined")]
    public string? JoinDate { get; init; }

    [Description("The credit score of the customer")]
    public int CreditScore { get; init; }
}
