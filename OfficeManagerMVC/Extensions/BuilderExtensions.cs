using FluentValidation;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Services;
using OfficeManagerMVC.Services.Interfaces;
using OfficeManagerMVC.Validation;

namespace OfficeManagerMVC.Extensions
{
    public static class BuilderExtensions
    {
        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IDepartmentService, DepartmentService>();
            services.AddHttpClient<IPositionsService, PositionsService>();
            services.AddHttpClient<IEmployeesService, EmployeesService>();
            services.AddHttpClient<IAuthService, AuthService>();
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IValidator<DepartmentDto>, DepartmentValidator>();
            services.AddScoped<IValidator<RegistrationRequestDto>, RegistrationRequestDtoValidator>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionsService, PositionsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
        }
    }
}
