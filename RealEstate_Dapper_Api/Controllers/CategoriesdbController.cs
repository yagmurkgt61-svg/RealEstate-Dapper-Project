using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_Api.Dtos.ProductsdbDtos;
using RealEstate_Dapper_Api.Models.Repositories.CategoriesRepositories;
using RealEstate_Dapper_Api.Models.Repositories.CategoryRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesdbController : ControllerBase
    {

        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesdbController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        [HttpGet]
        public async Task<IActionResult> CategoriesList()
        {
            var values = await _categoriesRepository.GetAllCategoriesAsync();
            return Ok(values);
        }
        [HttpPut("UpdateCategoriesdb")]
        public async Task<IActionResult> UpdateCategoriesdb(UpdateCategoriesdbDto updateCategoriesdbDto)
        {
            await _categoriesRepository.UpdateCategoriesdb(updateCategoriesdbDto);
            return Ok("Kategori Başarılı Bir Şekilde Güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoriesdb(CreateCategoriesdbDto createCategoriesdbDto)
        {
            await _categoriesRepository.CreateCategoriesdb(createCategoriesdbDto);
            return Ok("Kategori başarıyla eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriesdb(int id)
        {
            await _categoriesRepository.DeleteCategoriesdb(id);
            return Ok("Kategori Başarılı Bir Şekilde Silindi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesdbByCategoryId(int id)
        {
            var values = await _categoriesRepository.GetCategoriesdbByCategoryId(id);
            return Ok(values);
        }

    }
}
