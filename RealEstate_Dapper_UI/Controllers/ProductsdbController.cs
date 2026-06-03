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

            // 1. Önce Dropdown için tüm kategorileri API'den çekiyoruz
            var categoryResponse = await client.GetAsync("/api/Categoriesdb");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryData = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<RealEstate_Dapper_UI.Dtos.CategoriesdbDtos.ResultCategoriesdbDto>>(categoryData);
                ViewBag.CategoryList = categoryList;
            }

            // 2. Ardından mevcut ürün listesini çekiyoruz
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
        public async Task<IActionResult> CreateProductsdb([FromForm] CreateProductsdbDto createProductsdbDto)
        {
            // 1. Resim Dosyası Seçilmiş mi Kontrol Et ve Klasöre Kaydet
            if (createProductsdbDto.ImageFile != null && createProductsdbDto.ImageFile.Length > 0)
            {
                var resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                if (!Directory.Exists(resourcePath))
                {
                    Directory.CreateDirectory(resourcePath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createProductsdbDto.ImageFile.FileName);
                var filePath = Path.Combine(resourcePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createProductsdbDto.ImageFile.CopyToAsync(stream);
                }

                // DTO'ndaki alan adına (Image) uygun şekilde atama yapılıyor
                createProductsdbDto.Image = "/images/products/" + fileName;
            }
            else
            {
                createProductsdbDto.Image = "/images/no-image.png";
            }

            // 2. Hazırlanan DTO'yu API'ye JSON Olarak Gönder
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(createProductsdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("/api/Productsdb", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(createProductsdbDto);
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
            var productResponse = await client.GetAsync($"/api/Productsdb/{id}");
            if (productResponse.IsSuccessStatusCode)
            {
                var productData = await productResponse.Content.ReadAsStringAsync();
                var productValues = JsonConvert.DeserializeObject<UpdateProductsdbDto>(productData);
                return View(productValues);
            }

            return View();
         }

        [HttpPost]
        public async Task<IActionResult> UpdateProductsdb([FromForm] UpdateProductsdbDto updateProductsdbDto)
        {
            // 1. Kullanıcı yeni bir dosya yüklemiş mi kontrol et
            if (updateProductsdbDto.ImageFile != null && updateProductsdbDto.ImageFile.Length > 0)
            {
                var resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                if (!Directory.Exists(resourcePath))
                {
                    Directory.CreateDirectory(resourcePath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateProductsdbDto.ImageFile.FileName);
                var filePath = Path.Combine(resourcePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await updateProductsdbDto.ImageFile.CopyToAsync(stream);
                }

                // Yeni resim seçildiği için yeni yolu atıyoruz
                updateProductsdbDto.Image = "/images/products/" + fileName;
            }
            else
            {
                // Yeni resim SEÇİLMEDİYSE, formdaki gizli inputtan gelen mevcut (eski) resmi aynen koru
                // Böylece veritabanındaki eski resim linki null veya ezilmiş olmaz.
                // (Eğer hidden inputtan veri gelmiyorsa boş kalmasın diye varsayılan atayabilirsin)
                if (string.IsNullOrEmpty(updateProductsdbDto.Image))
                {
                    updateProductsdbDto.Image = "/images/no-image.png";
                }
            }

            // 2. API Güncelleme Çağrısı (PUT veya POST API mimarine göre düzenle - Genelde PutAsync kullanılır)
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(updateProductsdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // NOT: Senin API güncellemeyi PUT olarak bekliyorsa PutAsync, POST bekliyorsa PostAsync yap kanka.
            var responseMessage = await client.PostAsync("/api/Productsdb", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateProductsdbDto);
        }
    }
}
