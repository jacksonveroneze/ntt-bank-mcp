using System.Reflection;

namespace NttBank.Mcp.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
