using System.Reflection;

namespace NttBankMcp.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly =
        typeof(AssemblyReference).Assembly;
}
