using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace NttBankMcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class JsonOptionsExtensions
{
    public static IServiceCollection AddJsonOptionsSerialize(
        this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.PropertyNamingPolicy =
                JsonNamingPolicy.CamelCase;
            options.SerializerOptions.WriteIndented = false;
            options.SerializerOptions.PropertyNameCaseInsensitive = false;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}
