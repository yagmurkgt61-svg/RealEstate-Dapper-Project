using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstate_Dapper_Api.Containers;
using RealEstate_Dapper_Api.Hubs;
using RealEstate_Dapper_Api.Middlewares;
using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyAllowedOrigins",
//        policy =>
//        {
//            policy.WithOrigins("https://yagmurkagit.com/", "https://localhost:44302/") // UI adresin
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

// Add services to the container.


builder.Services.ContainerDependencies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(
                "https://yagmurkagit.com",
                "https://localhost:44302"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddHttpClient();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<TelegramService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Login/Index"; // Giriş yapılmamışsa yönlendirilecek sayfa
        options.LogoutPath = "/Login/Logout";
        options.Cookie.Name = "RealEstateCookie"; // Cookie adı
    });
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRHub>("/signalRHub");

app.MapGet("/", () => Results.Ok("API ayakta"));
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
//localhost:1234/swagger/category/index
//localhost:1234/signalrhub
app.Run();
