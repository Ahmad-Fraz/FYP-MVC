using DataBase;
using Encapsulation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<Models.Dashboard.news_events>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaims>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DBase>()
    .AddRoles<IdentityRole>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<SignUpModel>();
builder.Services.AddScoped<FYP.Models.CreateRoles>();
builder.Services.AddScoped<FYP.Models.Dashboard.Quizz>();
builder.Services.AddScoped<FYP.Models.Dashboard.Discussion>();
builder.Services.AddScoped<ContactUsModel>();
builder.Services.AddScoped<FYP.Models.Dashboard.CourseList>();
builder.Services.AddScoped<Microsoft.AspNetCore.Http.HttpContextAccessor>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//Authorization

builder.Services.ConfigureApplicationCookie(config =>
{
    config.AccessDeniedPath = new PathString("/Dashboard/AccessDenied");
    config.LoginPath = new PathString("/Account/SignIn");
});



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Dashboard}/{id?}");

app.Run();
