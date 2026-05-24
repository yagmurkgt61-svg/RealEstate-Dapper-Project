using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.ProductsdbRepositories
{
    public class ProductsdbRepository: IProductsdbRepository
    {
        private readonly Context _context;

        public ProductsdbRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateProductsdb(CreateProductsdbDto createProductsdbDto)
        {      
            string query = "Insert Into Productsdb (CategoryID,ProductName,UnitInStock,Price,PriceVat) values (@CategoryID,@ProductName,@UnitInStock,@Price,@PriceVat)";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", createProductsdbDto.CategoryID);
            parameters.Add("@ProductName", createProductsdbDto.ProductName);
            parameters.Add("@UnitInStock", createProductsdbDto.UnitInStock);
            parameters.Add("@Price", createProductsdbDto.Price);
            parameters.Add("@PriceVat", createProductsdbDto.PriceVat);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<List<ResultProductsdbDto>> GetAllProductsdbAsync()
        {
            // tabloda bulunan tüm kayıtları listeleyen sorgu
            string query = "Select * From Productsdb";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductsdbDto>(query);
                return values.ToList();

            }
        }
        public async Task UpdateProductsdb(UpdateProductsdbDto updateProductsdbDto)
        {
            string query = "Update Productsdb Set CategoryID=@CategoryID, ProductName=@ProductName, UnitInStock=@UnitInStock, Price=@Price, PriceVat=@PriceVat where ProductID=@ProductID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", updateProductsdbDto.CategoryID);
            parameters.Add("@ProductName", updateProductsdbDto.ProductName);
            parameters.Add("@UnitInStock", updateProductsdbDto.UnitInStock);
            parameters.Add("@Price", updateProductsdbDto.Price);
            parameters.Add("@PriceVat", updateProductsdbDto.PriceVat);
            parameters.Add("@ProductID", updateProductsdbDto.ProductID);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<GetProductsdbByProductIdDto> GetProductsdbByProductId(int id)
        {
            string query = "Select ProductID,CategoryID,ProductName,UnitInStock,Price,PriceVat From Productsdb where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductsdbByProductIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }
    }
}
