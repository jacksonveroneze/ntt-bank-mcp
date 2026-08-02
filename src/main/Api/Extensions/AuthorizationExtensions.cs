using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using NttBankMcp.Api.Security;
using NttBankMcp.Infrastructure.Configurations;

namespace NttBankMcp.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthorizationExtensions
{
    public static IServiceCollection AddAppAuthorization(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.JwtAccess, AddJwtRequirements);

            options.AddPolicy(AuthorizationPolicies.CustomerRead, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.CustomerRead);
            });

            options.AddPolicy(AuthorizationPolicies.CustomerAccountsRead, policy =>
            {
                AddJwtScopeRequirements(policy, AuthorizationScopes.CustomerAccountsRead);
            });
        });

        return services;
        
        static void AddJwtRequirements(AuthorizationPolicyBuilder policy)
        {
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
        }

        static void AddJwtScopeRequirements(
            AuthorizationPolicyBuilder policy,
            string requiredScope)
        {
            AddJwtRequirements(policy);
            policy.RequireAssertion(context =>
                HasScope(context.User, requiredScope, "scope", "scp"));
        }

        static bool HasScope(
            ClaimsPrincipal user,
            string requiredScope,
            params string[] claimTypes)
        {
            var scopeClaims = claimTypes
                .SelectMany(user.FindAll)
                .SelectMany(claim => claim.Value.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

            return scopeClaims.Contains(requiredScope, StringComparer.Ordinal);
        }
    }
}
