using NttBank.Mcp.Infrastructure.Configurations;
using NttBank.Mcp.Infrastructure.Extensions;

namespace NttBank.Mcp.Mcp.Extensions;

internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder Configure()
        {
            builder.Services.AddAppConfigs(builder.Configuration);

            var appConfiguration = builder.Configuration
                .Get<AppConfiguration>()!;

            builder.AddLogger(appConfiguration);

            builder.Services
                .AddAppAuthentication(appConfiguration)
                .AddAppAuthorization(appConfiguration)
                .AddMcp(appConfiguration)
                .AddJsonOptionsSerialize()
                .AddCorrelation()
                .AddApplicationServices()
                .AddHttpClient(appConfiguration)
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
                .AddOpenTelemetry(appConfiguration)
                .AddHealthChecks();

            return builder;
        }
    }
}
