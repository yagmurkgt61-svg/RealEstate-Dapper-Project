using RealEstate_Dapper_Api.Hubs;
using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Models.Repositories.AppUserRepositories;
using RealEstate_Dapper_Api.Models.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Models.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Models.Repositories.ContactRepositories;
using RealEstate_Dapper_Api.Models.Repositories.EmployeeRepositories;
using RealEstate_Dapper_Api.Models.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories;
using RealEstate_Dapper_Api.Models.Repositories.EstateAgentRepositories.DashboardRepositories.LastProductsRepositories;
using RealEstate_Dapper_Api.Models.Repositories.EstateAgentRepositories.DashboardRepositories.StatisticRepositories;
using RealEstate_Dapper_Api.Models.Repositories.MessageRepositories;
using RealEstate_Dapper_Api.Models.Repositories.PopularLocacitonRepositories;
using RealEstate_Dapper_Api.Models.Repositories.ProductImageRepositories;
using RealEstate_Dapper_Api.Models.Repositories.ProductRepository;
using RealEstate_Dapper_Api.Models.Repositories.PropertyAmenityRepositories;
using RealEstate_Dapper_Api.Models.Repositories.ServiceRepository;
using RealEstate_Dapper_Api.Models.Repositories.StatisticsRepositories;
using RealEstate_Dapper_Api.Models.Repositories.TestimonialRepositories;
using RealEstate_Dapper_Api.Models.Repositories.ToDoListRepositories;
using RealEstate_Dapper_Api.Models.Repositories.WhoWeAreRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("https://yagmurkagit.com/", "https://localhost:44302/") // UI adresin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddTransient<Context>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository,ProductRepository >();
builder.Services.AddTransient<IWhoWeAreDetailRepository, WhoWeAreDetailRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IBottomGridRepository, BottomGridRepository>();
builder.Services.AddTransient<IPopularLocationRepository, PopularLocationRepository>();
builder.Services.AddTransient<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddTransient<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IChartRepository, ChartRepository>();
builder.Services.AddTransient<ILast5ProductsRepository, Last5ProductsRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IProductImageRepository, ProductImageRepository>();
builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
builder.Services.AddTransient<IPropertyAmenityRepository, PropertyAmenityRepository>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddHttpClient();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyAllowedOrigins");

app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRHub>("/signalRHub");

app.MapGet("/", () => Results.Ok("API ayakta"));
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
//localhost:1234/swagger/category/index
//localhost:1234/signalrhub
app.Run();
