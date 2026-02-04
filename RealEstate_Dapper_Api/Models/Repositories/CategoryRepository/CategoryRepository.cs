using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;
using System.Threading.Tasks;

namespace RealEstate_Dapper_Api.Models.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _concext;

        public CategoryRepository(Context concext)
        {
            _concext = concext;
        }

        public async Task CreateCategory(CreateCategoryDto CategoryDto)
        {
            string query = "Insert Into Category (CategoryName,CategoryStatus) values (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("categoryName", CategoryDto.CategoryName);
            parameters.Add("categoryStatus", true);
            using (var connection = _concext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteCategory(int id)
        {
            string query = "Delete From Category where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("categoryID", id);
            using (var connection = _concext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }   
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category";
            using (var connection = _concext.CreateConnection())
            {
                var values  = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();

            }
        }

        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {
            string query = "Update Category Set CategoryName=@categoryName,CategoryStatus=@categoryStatus where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            using (var connectiont = _concext.CreateConnection()) { 
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
