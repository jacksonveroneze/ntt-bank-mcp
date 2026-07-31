using System.Reflection;

namespace NttBank.Mcp.Mcp;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
