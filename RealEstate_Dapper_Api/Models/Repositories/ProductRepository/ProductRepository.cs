using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
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

        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            string query = "Insert Into Product (Title,Price,CityId,CityName,District,CoverImage,Adress,Description,Type,DealOfTheDay,ProductStatus,AdvertisementDate,ProductCategory,AppUserId) values (@Title,@Price,@CityId,@CityName,@District,@CoverImage,@Adress,@Description,@Type,@DealOfTheDay,@ProductStatus,@AdvertisementDate,@ProductCategory,@AppUserId)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createProductDto.Title);
            parameters.Add("@Price", createProductDto.Price);
            parameters.Add("@CityId", createProductDto.CityId);
            parameters.Add("@CityName", createProductDto.CityName);
            parameters.Add("@District", createProductDto.District);
            parameters.Add("@CoverImage", createProductDto.CoverImage);
            parameters.Add("@Adress", createProductDto.Adress);
            parameters.Add("@Description", createProductDto.Description);
            parameters.Add("@Type", createProductDto.Type);
            parameters.Add("@DealOfTheDay", createProductDto.DealOfTheDay);
            parameters.Add("@ProductStatus", createProductDto.ProductStatus);
            parameters.Add("@AdvertisementDate", createProductDto.AdvertisementDate);
            parameters.Add("@ProductCategory", createProductDto.ProductCategory);
            parameters.Add("@AppUserId", createProductDto.AppUserId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
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
            string query = "Select ProductID,Title,Price,CityId,CityName,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID"; 
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<List<ResultLast3ProductWithCategoryDto>> GetLast3ProductAsync()
        {
            string query = "Select Top(3) ProductID,Title,Price,CityId,CityName,District,ProductCategory,CategoryName,AdvertisementDate,CoverImage,Description From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Order By ProductID Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast3ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            // tabloda bulunan son 5 kaydı Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select Top(5) ProductID,Title,Price,CityId,CityName,District,ProductCategory,CategoryName,AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryID Where Type=N'Kiralık' Order By ProductID Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            // tabloda bulunan ve EmployeeID'si belirlediğim id'ye eşit olan ve ProductStatus'ü false olan kayıtları Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select ProductID,Title,Price,CityId,CityName,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where AppUserId=@AapUserId and ProductStatus=0";
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
            string query = "Select ProductID,Title,Price,CityId,CityName,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where AppUserId=@AppUserId and ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@AppUserId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query,parameters);
                return values.ToList();

            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueWithCategoryAsync()
        {
            // tabloda bulunan tüm kayıtları Category tablosu ile ilişkilendirerek listeleyen sorgu
            string query = "Select ProductID,Title,Price,CityId,CityName,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where DealOfTheDay=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            string query = "Select ProductID,Title,Price,CityId,CityName,District,CategoryName,CoverImage,Type,Adress,DealOfTheDay,AdvertisementDate From Product LEFT JOIN Category on Product.ProductCategory=Category.CategoryID where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<GetProductByProductIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id)
        {
            string query = "Select * From ProductDetails Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductDetailByIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
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

        public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
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

        public async Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string cityName)
        {

            string query = "Select * From Product Where Title like '%" + @searchKeyValue + "%'and ProductCategory=@propertyCategoryId and CityName=@cityName";
            var parameters = new DynamicParameters();
            parameters.Add("@searchKeyValue", searchKeyValue);
            parameters.Add("@propertyCategoryId", propertyCategoryId);
            parameters.Add("@cityName", cityName);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithSearchListDto>(query, parameters);
                return values.ToList();
            }
        }

        public async void UpdateProduct(UpdateProductDto productDto)
        {
            // tabloda belirlediğim id'ye sahip kaydı güncelleyen sorgu
            string query = "Update Product Set Title=@title,ProductStatus=@productStatus where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@title", productDto.Title);
            parameters.Add("@productStatus", productDto.ProductStatus);
            parameters.Add("@ProductID", productDto.ProductStatus);
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
