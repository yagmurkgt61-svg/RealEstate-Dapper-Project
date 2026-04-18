using RealEstate_Dapper_Api.Dtos.CityDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.CityRespositories
{
    public interface ICityRepository
    {
        Task<List<ResultCityDto>> GetAllCityAsync();
    }
}
