using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _concext;

        public ProductRepository(Context concext)
        {
            _concext = concext;
        }
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From Product";
            using (var connection = _concext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();

            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            // Use LEFT JOIN so products without a category are also returned (CategoryName will be NULL)
            string query = "Select ProductID,Title,Price,City,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID"; 
            using (var connection = _concext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = "Select Top(5) ProductID,Title,Price,City,District,ProductCategory,CategoryName,AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type='Kiralık' Order By ProductID Desc";
            using (var connection = _concext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsync(int id)
        {
            string query = "Select ProductID,Title,Price,City,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where EmployeeID=@employeeID";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _concext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query,parameters);
                return values.ToList();

            }
        }

        public async void ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "Update Product Set DealOfTheDay=0 Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _concext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            string query = "Update Product Set DealOfTheDay=1 Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _concext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
