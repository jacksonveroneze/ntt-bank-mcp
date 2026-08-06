namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public sealed record TransactionGroupSummaryResponse
{
    public string? GroupKey { get; init; }
    
    public decimal? TotalAmount { get; init; }
    
    public long? TransactionCount { get; init; }
    
    public decimal? AverageAmount { get; init; }
}
