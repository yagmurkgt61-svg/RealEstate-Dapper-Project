using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");

            var response = await client.GetAsync("api/Categoriesdb");

            var jsonData = await response.Content.ReadAsStringAsync();

            var values = JsonConvert.DeserializeObject<List<ResultCategoriesdbDto>>(jsonData);

            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategories()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategories(CreateCategoriesdbDto createCategoriesdbDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(createCategoriesdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Categoriesdb", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategories(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/Categoriesdb/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategoriesdbDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategories(UpdateCategoriesdbDto updateCategoriesdbDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(updateCategoriesdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("/api/Categoriesdb/UpdateCategoriesdb", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(updateCategoriesdbDto);

        }
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.DeleteAsync($"/api/Categoriesdb/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
