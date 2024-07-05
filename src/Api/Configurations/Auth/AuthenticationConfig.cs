using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Configurations.Auth;

public static class AuthenticationConfig
{
    private const string OauthConfigSectionName = "OAuth";
    public static void AddAuthnticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var oauthSettings = new OAuthSettings();
        configuration.GetSection(OauthConfigSectionName).Bind(oauthSettings);
        
        services.AddAuthentication(o => {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;
                cfg.Authority =  oauthSettings.Authority;
                cfg.IncludeErrorDetails = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = oauthSettings.Authority,
                    ValidateLifetime = true
                };

                cfg.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                };
            });
    }
}