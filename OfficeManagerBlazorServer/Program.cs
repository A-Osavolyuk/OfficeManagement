using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using OfficeManagerBlazorServer;
using OfficeManagerBlazorServer.Common;
using OfficeManagerBlazorServer.Components;
using OfficeManagerBlazorServer.Models.DTOs;
using OfficeManagerBlazorServer.Services;
using OfficeManagerBlazorServer.Services.Interfaces;
using OfficeManagerBlazorServer.Validation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<HttpData>(builder.Configuration.GetSection("HttpData"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IDepartmentHttpService, DepartmentHttpService>();
builder.Services.AddHttpClient<IAuthHttpService, AuthHttpService>();

builder.Services.AddScoped<IAuthHttpService, AuthHttpService>();
builder.Services.AddScoped<IBaseHttpService, BaseHttpService>();
builder.Services.AddScoped<IDepartmentHttpService, DepartmentHttpService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

var jwtOptions = builder.Configuration.GetSection("JWT-Options");

var secret = jwtOptions.GetValue<string>("Secret");
var issuer = jwtOptions.GetValue<string>("Issuer");
var audience = jwtOptions.GetValue<string>("Audience");

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
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

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
