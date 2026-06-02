using RealEstate_Dapper_Api.Models.DapperContext;
using RealEstate_Dapper_Api.Models.Repositories.AppUserRepositories;
using RealEstate_Dapper_Api.Models.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Models.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Models.Repositories.CityRespositories;
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
using RealEstate_Dapper_Api.Models.Repositories.SubFeatureRepositories;
using RealEstate_Dapper_Api.Models.Repositories.TestimonialRepositories;
using RealEstate_Dapper_Api.Models.Repositories.ToDoListRepositories;
using RealEstate_Dapper_Api.Models.Repositories.WhoWeAreRepository;
using RealEstate_Dapper_Api.Models.Repositories.ProductsdbRepositories;
using RealEstate_Dapper_Api.Models.Repositories.CategoriesRepositories;

namespace RealEstate_Dapper_Api.Containers
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services) 
        {
            services.AddTransient<Context>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IWhoWeAreDetailRepository, WhoWeAreDetailRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IBottomGridRepository, BottomGridRepository>();
            services.AddTransient<IPopularLocationRepository, PopularLocationRepository>();
            services.AddTransient<ITestimonialRepository, TestimonialRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<ILast5ProductsRepository, Last5ProductsRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IPropertyAmenityRepository, PropertyAmenityRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ISubFeatureRepository, SubFeatureRepository>();
            services.AddTransient<IProductsdbRepository, ProductsdbRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        }
    }
}
