using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using OfficeManagerBlazorServer;
using OfficeManagerBlazorServer.Common;
using OfficeManagerBlazorServer.Components;
using OfficeManagerBlazorServer.Models.DTOs;
using OfficeManagerBlazorServer.Services;
using OfficeManagerBlazorServer.Services.Interfaces;
using OfficeManagerBlazorServer.Validation;

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
