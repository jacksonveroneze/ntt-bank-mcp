using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Infrastructure.Configurations;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace NttBankMcp.Infrastructure.Extensions;

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
            .AddMetrics(appConfiguration)
            .AddTracing(appConfiguration);

        return services;

        void ConfigureResource(ResourceBuilder r)
        {
            r.AddService(
                appConfiguration.Application.Name,
                serviceVersion: appConfiguration.Application.Version.ToString(),
                serviceInstanceId: Environment.MachineName);
        }
    }

    extension(IOpenTelemetryBuilder builder)
    {
        private IOpenTelemetryBuilder AddMetrics(
            AppConfiguration appConfiguration)
        {
            builder.WithMetrics(opts => opts
                .AddMeter(appConfiguration.Application.Name)
                .AddProcessInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter());

            return builder;
        }
        
        private IOpenTelemetryBuilder AddTracing(
            AppConfiguration appConfiguration)
        {
            builder.WithTracing(tracing =>
            {
                tracing
                    .AddSource(appConfiguration.Application.Name)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

            return builder;
        }
    }
}
