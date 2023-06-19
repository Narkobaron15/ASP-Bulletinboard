using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Bulletinboard.Helpers;
using Business_Logic.Interfaces;
using Business_Logic.Services;
using Business_Logic.Profiles;
using Repository.Implementations;
using Repository.Interfaces;
using Business_Logic.DTO;

var builder = WebApplication.CreateBuilder(args);

// Retrieval of connection strings
string? dbConStr = builder.Configuration.GetConnectionString("AzureDb"),
        fsConStr = builder.Configuration.GetConnectionString("AzureFS");

// Add services to the container.
builder.Services.AddControllersWithViews();


// ------------ Injections start

// DbContext injection
builder.Services.AddDbContext<ShopDbContext>(optBuilder => optBuilder.UseSqlServer(dbConStr));

// Services injections
builder.Services.AddScoped<IGenericUnitOfWork, EFUnitOfWork<ShopDbContext>>();
builder.Services.AddScoped<IBulletinService, BulletinService>();
builder.Services.AddScoped<IDataService<Category, CategoryDTO>, DataService<Category, CategoryDTO>>();
builder.Services.AddScoped<IDataService<City, CityDTO>, DataService<City, CityDTO>>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IFileService, AzurePictureService>();

builder.Services.AddAutoMapper(typeof(ApplicationProfile));

// Identity configurations
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ShopDbContext>();

// ------------ Injections end


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Seed roles and admin user
using var scope = app.Services.CreateScope();
var provider = scope.ServiceProvider;
var seedingtask = StartupHelpers.SeedRoles(provider)
                                .ContinueWith(async x => await StartupHelpers.SeedAdmin(provider));

app.UseExceptionHandler("/Error");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

await await seedingtask;

app.Run();
