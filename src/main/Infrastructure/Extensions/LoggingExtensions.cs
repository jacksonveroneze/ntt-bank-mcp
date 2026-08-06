using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using NttBankMcp.Infrastructure.Configurations;
using Serilog;

namespace NttBankMcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogger(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Host.UseSerilog((hostingContext,
            services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName",
                    appConfiguration.Application.Name)
                .Enrich.WithProperty("ApplicationVersion",
                    appConfiguration.Application.Version.ToString());
        });

        
        // builder.Services.AddLogging(logging => logging.AddOpenTelemetry(openTelemetryLoggerOptions =>
        // {
        //     openTelemetryLoggerOptions.SetResourceBuilder(
        //         ResourceBuilder.CreateEmpty()
        //             .AddService(appConfiguration.Application.Name,
        //                 serviceVersion: appConfiguration.Application.Version.ToString()));
        //
        //     openTelemetryLoggerOptions.IncludeScopes = true;
        //     openTelemetryLoggerOptions.IncludeFormattedMessage = true;
        //
        //     openTelemetryLoggerOptions.AddOtlpExporter(exporter =>
        //     {
        //         exporter.Endpoint = new Uri("http://localhost:15341/ingest/otlp/v1/logs");
        //         exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
        //         //exporter.Headers = "X-Seq-ApiKey=abcde12345";
        //     });
        // }));
        
        return builder;
    }
}
