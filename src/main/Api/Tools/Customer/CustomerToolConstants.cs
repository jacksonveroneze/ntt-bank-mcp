namespace NttBankMcp.Api.Tools.Customer;

/// <summary>
/// Descrições de parâmetros compartilhadas pelas tools de customer.
/// São superfície de decisão do modelo — mantenha-as em inglês e consistentes
/// entre as tools.
/// </summary>
internal static class CustomerToolConstants
{
    public const string CustomerIdParamDesc =
        "The unique numeric identifier of the customer (must be greater than zero).";
}
