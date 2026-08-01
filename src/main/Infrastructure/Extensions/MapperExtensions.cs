using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Application;

namespace NttBankMcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services, Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.RequireExplicitMapping = true;
        config.RequireDestinationMemberSource = true;

        config.Scan(typeof(MapperExtensions).Assembly);
        config.Scan(typeof(AssemblyReference).Assembly);

        services.AddSingleton(config);

        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
