using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/Products/GetProductByProductId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                await GetCategoriesToViewBag();
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent =
                new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage =
                await client.PutAsync("/api/Products/UpdateProduct", stringContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = responseContent;
            return View(updateProductDto);
        }
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/Products/ProductDealOfTheDayStatusChangeToFalse/"+id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/Products/ProductDealOfTheDayStatusChangeToTrue/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            await GetCategoriesToViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Products", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            await GetCategoriesToViewBag();
            return View(createProductDto);
        }

        private async Task GetCategoriesToViewBag()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Categories"); 
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.v = categoryValues;
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/ProductDetails/GetProductDetailByProductId?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetProductByProductIdDto>(jsonData);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.DeleteAsync($"/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
