using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBank.Mcp.Application.Abstractions.Repositories;
using NttBank.Mcp.Application.Abstractions.Services;
using NttBank.Mcp.Application.Customers.GetCustomer;
using NttBank.Mcp.Infrastructure.Repositories.Order;
using NttBank.Mcp.Infrastructure.Services;

namespace NttBank.Mcp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IGetCustomerUseCase, GetCustomerUseCase>();

        return services;
    }
}
