using System.Reflection;

namespace NttBankMcp.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
