using RealEstate_Dapper_Api.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.CategoriesRepositories
{
    public interface ICategoriesRepository
    {
        Task<List<ResultCategoriesdbDto>> GetAllCategoriesAsync();
        Task CreateCategoriesdb(CreateCategoriesdbDto createCategoriesdbDto);
        Task UpdateCategoriesdb(UpdateCategoriesdbDto updateCategoriesdbDto);
        Task<GetByIdCategoriesDto> GetCategoriesdbByCategoryId(int id);
        Task DeleteCategoriesdb(int id);
        Task<List<ResultCategoriesdbDto>> GetActiveCategoriesAsync();

    }
}
