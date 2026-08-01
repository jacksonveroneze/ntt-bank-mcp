using NttBankMcp.Infrastructure.Configurations;
using NttBankMcp.Infrastructure.Extensions;

namespace NttBankMcp.Mcp.Extensions;

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
                .AddCultureConfiguration()
                .AddHttpClient(appConfiguration)
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
                .AddOpenTelemetry(appConfiguration)
                .AddHealthChecks();

            return builder;
        }
    }
}
