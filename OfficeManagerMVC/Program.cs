using FluentValidation;
using FluentValidation.AspNetCore;
using OfficeManagerMVC.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Services;
using OfficeManagerMVC.Services.Interfaces;
using OfficeManagerMVC.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation();

builder.Services.Configure<HttpData>(builder.Configuration.GetSection("HttpData"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IDepartmentHttpService, DepartmentHttpService>();

builder.Services.AddScoped<IValidator<DepartmentDto>, DepartmentValidator>();
builder.Services.AddScoped<IBaseHttpService, BaseHttpService>();
builder.Services.AddScoped<IDepartmentHttpService, DepartmentHttpService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
