    using FreeCourse.Shared.Services;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Models;
    using FreeCourse.Web.Services.Abstract;
    using FreeCourse.Web.Services.Interface;
    using Microsoft.AspNetCore.Authentication.Cookies;

    var builder = WebApplication.CreateBuilder(args);

var serviceapisets=builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
    // Add services to the container.
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Auth/SignIn";
        opt.ExpireTimeSpan = TimeSpan.FromDays(60);
        opt.SlidingExpiration = true;
        opt.Cookie.Name = "freecoursewebcookie";
    });
    builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
    builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceapisets.GatewayURl}/{serviceapisets.Catalog.Path}");
});
    builder.Services.AddHttpClient<IIdentityService,IdentityService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
    builder.Services.AddControllersWithViews();
    builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
    builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceapisets.IdentityBaseURL);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
