using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoriesdbDtos;
using RealEstate_Dapper_UI.Dtos.ProductsdbDtos;
using System.Globalization;
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
            var categoryResponse = await client.GetAsync("/api/Categoriesdb");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryData = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<RealEstate_Dapper_UI.Dtos.CategoriesdbDtos.ResultCategoriesdbDto>>(categoryData);
                ViewBag.CategoryList = categoryList;
            }
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
            SetProductPricesFromForm(createProductsdbDto);
            // kullanıcı resim için dosya seçmiş mi kontrol et
            if (createProductsdbDto.ImageFile != null && createProductsdbDto.ImageFile.Length > 0)
            {
                // Dosya yükleme işlemi için gerekli yolları ve dosya adını oluştur
                var resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
                // Eğer klasör yoksa oluştur
                if (!Directory.Exists(resourcePath))
                {
                    Directory.CreateDirectory(resourcePath);
                }
                // Dosya adlarının çakışmaması için benzersiz bir dosya adı oluştur
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createProductsdbDto.ImageFile.FileName);
                // Dosyayı belirtilen yola kaydet
                var filePath = Path.Combine(resourcePath, fileName);
                // Dosyayı kaydetme işlemi
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createProductsdbDto.ImageFile.CopyToAsync(stream);
                }
                // dto içine resmin yolunu yazar
                createProductsdbDto.Image = "/images/products/" + fileName;
            }
            else
            {
                // resim seçilmemişse varsayılan resim ataması yap
                createProductsdbDto.Image = "/images/no-image.png";
            }
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
            SetProductPricesFromForm(updateProductsdbDto);

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

                updateProductsdbDto.Image = "/images/products/" + fileName;
            }
            else
            {
    
                if (string.IsNullOrEmpty(updateProductsdbDto.Image))
                {
                    updateProductsdbDto.Image = "/images/no-image.png";
                }
            }

            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var jsonData = JsonConvert.SerializeObject(updateProductsdbDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("/api/Productsdb/UpdateProductsdb", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateProductsdbDto);
        }

        private void SetProductPricesFromForm(CreateProductsdbDto productDto)
        {
            if (TryReadDecimalFromForm("Price", out var price))
            {
                productDto.Price = price;
            }

            if (TryReadDecimalFromForm("PriceVat", out var priceVat))
            {
                productDto.PriceVat = priceVat;
            }
        }

        private void SetProductPricesFromForm(UpdateProductsdbDto productDto)
        {
            if (TryReadDecimalFromForm("Price", out var price))
            {
                productDto.Price = price;
            }

            if (TryReadDecimalFromForm("PriceVat", out var priceVat))
            {
                productDto.PriceVat = priceVat;
            }
        }

        private bool TryReadDecimalFromForm(string key, out decimal value)
        {
            value = 0;

            if (!Request.Form.TryGetValue(key, out var formValue))
            {
                return false;
            }

            return decimal.TryParse(
                formValue.ToString(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out value);
        }
    }
}
