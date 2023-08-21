using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Presentation;
using Repositories;
using Repositories.Contracts;
using Repositories.EntityFramework;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using StoreApp.Models;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureSession();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureServiceRegistration();
builder.Services.AddScoped<Cart>(c => SessionCart.GetCart(c));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRouting();
builder.Services.ConfigureApplicationCookie();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
app.ConfigureLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.MapAreaControllerRoute(
    name: "Admin", 
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.ConfigureAndCheckMigration();
app.ConfigureDefaultAdminUser();
app.MapRazorPages();
app.Run();