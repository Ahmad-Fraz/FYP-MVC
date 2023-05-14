using DataBase;
using Encapsulation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<Models.Dashboard.news_events>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaims>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DBase>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<SignUpModel>();
builder.Services.AddScoped<ContactUsModel>();
builder.Services.AddScoped<Microsoft.AspNetCore.Http.HttpContextAccessor>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContextPool<DBase>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddScoped<Interface, InterfaceImplementaion>();
builder.Services.AddHttpContextAccessor();

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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
