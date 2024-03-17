using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator.OtpNet;
using Core.Security.OtpAuthenticator;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Core.Security;


public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices<TUserId, TOperationClaimId, TRefreshTokenId>(
        this IServiceCollection services,
        TokenOptions tokenOptions
    )
    {
        services.AddSingleton<TokenOptions>(tokenOptions);
        services.AddScoped<
            ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>,
            JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId>
        >();
        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();

        return services;
    }
}