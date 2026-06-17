using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.CategoriesRepositories
{
    public class CategoriesRepository: ICategoriesRepository
    {
        private readonly Context _context;

        public CategoriesRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateCategoriesdb(CreateCategoriesdbDto createCategoriesdbDto)
        {
            string query = "Insert Into Categoriesdb (CategoryName, IsActive, [Order]) values (@CategoryName, @IsActive, @Order)";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryName", createCategoriesdbDto.CategoryName);
            parameters.Add("@IsActive", createCategoriesdbDto.IsActive);
            parameters.Add("@Order", createCategoriesdbDto.Order);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCategoriesdbDto>> GetAllCategoriesAsync()
        {
            string query = "Select * From Categoriesdb";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoriesdbDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetByIdCategoriesDto> GetCategoriesdbByCategoryId(int id)
        {

            string query = "Select CategoryID, CategoryName, IsActive, [Order] From Categoriesdb where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetByIdCategoriesDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task UpdateCategoriesdb(UpdateCategoriesdbDto updateCategoriesdbDto)
        {
            string query = "Update Categoriesdb Set CategoryName=@CategoryName, IsActive=@IsActive, [Order]=@Order where CategoryID=@CategoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", updateCategoriesdbDto.CategoryID);
            parameters.Add("@CategoryName", updateCategoriesdbDto.CategoryName);
            parameters.Add("@IsActive", updateCategoriesdbDto.IsActive);
            parameters.Add("@Order", updateCategoriesdbDto.Order);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCategoriesdb(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydı silen sorgu
            string query = "Delete From Categoriesdb where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<List<ResultCategoriesdbDto>> GetActiveCategoriesAsync()
        {
            // Sadece aktif (IsActive = 1) olan kategorileri çeken sorgu
            string query = "Select * From Categoriesdb Where IsActive = 1";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoriesdbDto>(query);
                return values.ToList();
            }
        }
    }
}
