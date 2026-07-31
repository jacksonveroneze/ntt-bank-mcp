using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBank.Mcp.Infrastructure.Configurations;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace NttBank.Mcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
        {
            options.Filter = ctx =>
                (!ctx.Request.Path.Value?.StartsWith("/metrics",
                    StringComparison.OrdinalIgnoreCase) ?? false) &&
                ctx.Request.Path != "/health";
        });

        services.AddOpenTelemetry()
            .ConfigureResource(ConfigureResource)
            .AddMetrics();

        return services;

        void ConfigureResource(ResourceBuilder r)
        {
            r.AddService(
                appConfiguration.Application!.Name!,
                serviceVersion: appConfiguration.Application!.Version!.ToString(),
                serviceInstanceId: Environment.MachineName);
        }
    }

    extension(IOpenTelemetryBuilder builder)
    {
        private IOpenTelemetryBuilder AddMetrics()
        {
            builder.WithMetrics(opts => opts
                .AddMeter("orders-agent")
                .AddProcessInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter());

            return builder;
        }
        
        private IOpenTelemetryBuilder AddTracing()
        {
            builder.WithTracing(tracing =>
            {
                tracing
                    .AddSource("orders-agent")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

            return builder;
        }
    }
}
