using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Abstractions.Services;
using NttBankMcp.Application.Customers.GetCustomer;
using NttBankMcp.Infrastructure.Repositories;
using NttBankMcp.Infrastructure.Services;

namespace NttBankMcp.Infrastructure.Extensions;

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
