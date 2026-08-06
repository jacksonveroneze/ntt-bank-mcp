using NttBankMcp.Infrastructure.Configurations;
using NttBankMcp.Infrastructure.Extensions;

namespace NttBankMcp.Api.Extensions;

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
                .AddHttpClient(appConfiguration)
                .AddCached(appConfiguration)
                .AddMcp(appConfiguration)
                .AddJsonOptionsSerialize()
                .AddCorrelation()
                .AddCultureConfiguration()
                .AddApplicationServices()
                .AddFluentValidation(AssemblyReference.Assembly)
                .AddMapper(AssemblyReference.Assembly)
                .AddOpenTelemetry(appConfiguration)
                .AddHealthCheck(appConfiguration);

            return builder;
        }
    }
}
