using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.ProductsdbRepositories
{
    public interface IProductsdbRepository
    {
        Task<List<ResultProductsdbDto>> GetAllProductsdbAsync();
        Task CreateProductsdb(CreateProductsdbDto createProductsdbDto);
        Task UpdateProductsdb(UpdateProductsdbDto updateProductsdbDto);
        Task<GetProductsdbByProductIdDto> GetProductsdbByProductId(int id);
    }
}
