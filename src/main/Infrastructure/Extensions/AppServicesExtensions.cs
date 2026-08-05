using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NttBankMcp.Application.Abstractions.Repositories;
using NttBankMcp.Application.Abstractions.Services;
using NttBankMcp.Application.Accounts.GetAccount;
using NttBankMcp.Application.Accounts.ListCustomerAccounts;
using NttBankMcp.Application.Accounts.ListAccountTransactions;
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

        #region Costumer

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGetCustomerUseCase, GetCustomerUseCase>();

        #endregion

        #region Account

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IGetAccountUseCase, GetAccountUseCase>();
        services.AddScoped<IListAccountTransactionsUseCase, ListAccountTransactionsUseCase>();
        services.AddScoped<IListCustomerAccountsUseCase, ListCustomerAccountsUseCase>();

        #endregion

        #region Branch

        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBranchCacheRepository, BranchCacheRepository>();

        #endregion

        #region Card

        services.AddScoped<ICardRepository, CardRepository>();

        #endregion

        #region Loan

        services.AddScoped<ILoanRepository, LoanRepository>();

        #endregion

        return services;
    }
}
