using CorrelationId;

namespace NttBank.Mcp.Mcp.Extensions;

internal static class WebApplicationExtensions
{
    private const string PathHealth = "/health";
    private const string PathMetrics = "metrics";
    private const string PathMcp = "/mcp";
    
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseCorrelationId();
        
        app.UseRouting();

        app.UseHealthChecks(PathHealth);
        app.UseOpenTelemetryPrometheusScrapingEndpoint(PathMetrics);

        //app.UseAuthentication();
        //app.UseAuthorization();

        app.MapMcp(PathMcp);
        
        return app;
    }
}
