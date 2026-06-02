using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;
using RealEstate_Dapper_Api.Models.Repositories.ProductsdbRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsdbController : ControllerBase
    {
        private readonly IProductsdbRepository _productsdbRepository;

        public ProductsdbController(IProductsdbRepository productsdbRepository)
        {
            _productsdbRepository = productsdbRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productsdbRepository.GetAllProductsdbAsync();
            return Ok(values);
        }
        [HttpPut("UpdateProductsdb")]
        public async Task<IActionResult> UpdateProductsdb(UpdateProductsdbDto updateProductsdbDto)
        {
            await _productsdbRepository.UpdateProductsdb(updateProductsdbDto);
            return Ok("Ürün Başarılı Bir Şekilde Güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductsdb(CreateProductsdbDto createProductsdbDto)
        {
            await _productsdbRepository.CreateProductsdb(createProductsdbDto);
            return Ok("Ürün başarıyla eklendi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsdbByProductId(int id)
        {
            var values = await _productsdbRepository.GetProductsdbByProductId(id);
            return Ok(values);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsdb(int id)
        {
            await _productsdbRepository.DeleteProductsdb(id);
            return Ok("Ürün Başarılı Bir Şekilde Silindi");
        }
    }
}
