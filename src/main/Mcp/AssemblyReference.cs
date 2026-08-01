using System.Reflection;

namespace NttBankMcp.Mcp;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
