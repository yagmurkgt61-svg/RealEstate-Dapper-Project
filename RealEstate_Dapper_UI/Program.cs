using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstate_Dapper_UI.Middlewares;
using RealEstate_Dapper_UI.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? throw new Exception("Api BaseUrl bulunamadı!");


builder.Services.AddHttpClient<TelegramService>();

builder.Services.AddHttpClient("RealEstateClient", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.LogoutPath = "/Login/LogOut";
        opt.AccessDeniedPath = "/Login/AccessDenied";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "RealEstateJwt";
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ROUTES
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}"
);

app.Run();