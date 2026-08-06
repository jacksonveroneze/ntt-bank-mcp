namespace NttBankMcp.Application.Accounts.SummarizeAccountTransactions;

public sealed record SummarizeAccountTransactionsResponse
{
    public int AccountId { get; init; }
    
    public string? GroupBy { get; init; }
    
    public DateTime? PeriodFrom { get; init; }
    
    public DateTime? PeriodTo { get; init; }
    
    public decimal? TotalCredits { get; init; }
    
    public decimal? TotalDebits { get; init; }
    
    public decimal? NetFlow { get; init; }
    
    public long? TotalTransactionsCount { get; init; }
    
    public IReadOnlyCollection<TransactionGroupSummaryResponse> Groups { get; init; } = [];
}
