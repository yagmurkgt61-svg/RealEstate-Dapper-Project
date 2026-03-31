using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            // tabloda bulunan tüm kayıtları listeleyen sorgu
            string query = "Select * From Product";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();

            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            // tabloda bulunan tüm kayıtları Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select ProductID,Title,Price,City,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID"; 
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            // tabloda bulunan son 5 kaydı Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select Top(5) ProductID,Title,Price,City,District,ProductCategory,CategoryName,AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type='Kiralık' Order By ProductID Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            // tabloda bulunan ve EmployeeID'si belirlediğim id'ye eşit olan ve ProductStatus'ü false olan kayıtları Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select ProductID,Title,Price,City,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where EmployeeID=@employeeID and ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();

            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            // tabloda bulunan ve EmployeeID'si belirlediğim id'ye eşit olan ve ProductStatus'ü true olan kayıtları Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select ProductID,Title,Price,City,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where EmployeeID=@employeeID and ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query,parameters);
                return values.ToList();

            }
        }

        public async void ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydın DealOfTheDay alanını false yapan sorgu
            string query = "Update Product Set DealOfTheDay=0 Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydın DealOfTheDay alanını true yapan sorgu
            string query = "Update Product Set DealOfTheDay=1 Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
