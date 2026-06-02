using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_UI.Dtos.ProductsdbDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ProductsdbController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsdbController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Productsdb");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductsdbDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultProductsdbDto>());
        }
        [HttpGet]
        public async Task<IActionResult> CreateProductsdb()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Categoriesdb");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<RealEstate_Dapper_UI.Dtos.CategoriesdbDtos.ResultCategoriesdbDto>>(jsonData);
                List<SelectListItem> categories = (from x in categoryList
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,   
                                                       Value = x.CategoryID.ToString() 
                                                   }).ToList();

                ViewBag.CategoryList = categories;
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductsdb(CreateProductsdbDto createProductsdbDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(createProductsdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Productsdb", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteProductsdb(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.DeleteAsync($"/api/Productsdb/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProductsdb(int id)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");

            // A. İlk olarak Dropdown için kategorileri API'den çekiyoruz
            var categoryResponse = await client.GetAsync("/api/Categoriesdb");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryData = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<RealEstate_Dapper_UI.Dtos.CategoriesdbDtos.ResultCategoriesdbDto>>(categoryData);

                List<SelectListItem> categories = (from x in categoryList
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
                ViewBag.CategoryList = categories;
            }

            // B. Şimdi de güncellenecek ürünün kendi verilerini API'den çekiyoruz
            // (Not: API'nizdeki tekil ürün getiren endpoint'inize göre URL'i düzenleyebilirsiniz, örn: /api/Productsdb/{id})
            var productResponse = await client.GetAsync($"/api/Productsdb/{id}");
            if (productResponse.IsSuccessStatusCode)
            {
                var productData = await productResponse.Content.ReadAsStringAsync();
                var productValues = JsonConvert.DeserializeObject<UpdateProductsdbDto>(productData);
                return View(productValues);
            }

            return View();
        }

        // 5. Ürün Güncelleme İşlemi (Butona Basıldığında)
        [HttpPost]
        public async Task<IActionResult> UpdateProductsdb(UpdateProductsdbDto updateProductsdbDto)
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");

            var jsonData = JsonConvert.SerializeObject(updateProductsdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // ÇÖZÜM: API tarafındaki rota isminize uygun olarak endpoint adresini güncelledik.
            // Eğer API tarafındaki metodunuzun üzerinde [HttpPut("UpdateProductsdb")] yazıyorsa bu adres tam isabet çalışacaktır.
            var responseMessage = await client.PutAsync("/api/Productsdb/UpdateProductsdb", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Eğer API başarısız yanıt dönerse hatayı görebilmek için View'a veriyi geri gönderiyoruz
            return View(updateProductsdbDto);
        }
    }
}
