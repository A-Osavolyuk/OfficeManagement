using OfficeManagerBlazorServer.Services.Interfaces;
using OfficeManagerBlazorServer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OfficeManagerBlazorServer.Extensions
{
    public static class BuilderExtensions
    {
        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IDepartmentHttpService, DepartmentHttpService>();
            services.AddHttpClient<IAuthHttpService, AuthHttpService>();
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthHttpService, AuthHttpService>();
            services.AddScoped<IBaseHttpService, BaseHttpService>();
            services.AddScoped<IDepartmentHttpService, DepartmentHttpService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
        }

        public static void ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JWT-Options");

            var secret = jwtOptions.GetValue<string>("Secret");
            var issuer = jwtOptions.GetValue<string>("Issuer");
            var audience = jwtOptions.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };
            });
        }
    }
}
