using OfficeManagerBlazorServer.Services.Interfaces;
using OfficeManagerBlazorServer.Services;
using Microsoft.AspNetCore.Components.Authorization;

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
    }
}
