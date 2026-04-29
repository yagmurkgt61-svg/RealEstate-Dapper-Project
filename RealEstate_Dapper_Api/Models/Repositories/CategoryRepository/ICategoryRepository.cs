using RealEstate_Dapper_Api.Dtos.CategoryDtos;
namespace RealEstate_Dapper_Api.Models.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategory();
        Task CreateCategory(CreateCategoryDto CategoryDto);
        Task DeleteCategory(int id);
        Task UpdateCategory(UpdateCategoryDto categoryDto);
        Task<GetByIDCategoryDto> GetCategory(int id);
    }
}   