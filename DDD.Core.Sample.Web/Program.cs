using DDD.Core.Sample.Web.Filter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
builder.WebHost.UseNLog();

var services = builder.Services;

DDD.Core.Sample.BootStrapper.Startup.AddConfigureServices(services, builder.Configuration);


services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = new PathString("/Start/Login");
    options.AccessDeniedPath = new PathString("/Start/AccessDenied");
});

services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(ModelStateValidationAttribute));

    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    // 因为是后台系统，必须登陆以后才能操作
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
DDD.Core.Sample.BootStrapper.Startup.AddConfigure();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();