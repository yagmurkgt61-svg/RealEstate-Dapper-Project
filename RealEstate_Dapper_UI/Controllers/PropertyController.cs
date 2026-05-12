using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Dtos.PropertyDtos;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
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
        public async Task<IActionResult> PropertyListWithSearch(string searchKeyValue, int propertyCategoryId, string city)
        { 
            ViewBag.searchKeyValue = TempData["searchKeyValue"];
            ViewBag.propertyCategoryId = TempData["propertyCategoryId"];
            ViewBag.city = TempData["city"];

            searchKeyValue = TempData["searchKeyValue"].ToString();
            propertyCategoryId = int.Parse(TempData["propertyCategoryId"].ToString());
            city = TempData["city"].ToString();
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync($"/api/Products/ResultProductWithSearchList?searchKeyValue={searchKeyValue}&propertyCategoryId={propertyCategoryId}&city={city}");
            if (responseMessage.IsSuccessStatusCode) 
            { 
                var jsonData= await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductWithSearchListDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet("property/{slug}/{id}")]
        public async Task<IActionResult> PropertySingle(string slug,int id) 
        {
            ViewBag.i = id;
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Products/GetProductByProductId/" + id);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);
            if (values == null || values.productID == 0)
            {
                return NotFound();
            }

            var client2 = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage2 = await client2.GetAsync("/api/ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2);

            ViewBag.productId = values.productID;
            ViewBag.title1 = values.title.ToString();
            ViewBag.price = values.price;
            ViewBag.city = values.cityName;
            ViewBag.district = values.district;
            ViewBag.adress = values.adress;  
            ViewBag.type = values.type; 
            ViewBag.description = values.description;
            ViewBag.slugUrl = values.SlugUrl;
            ViewBag.bathCount = values2?.bathCount ?? 0;
            ViewBag.bedCount = values2?.bedRoomCount ?? 0;
            ViewBag.size = values2?.productSize ?? 0;
            ViewBag.roomCount = values2?.roomCount ?? 0;
            ViewBag.garageCount = values2?.garageSize ?? 0;
            ViewBag.buildYear = values2?.buildYear;
            ViewBag.date = values.AdvertisementDate;
            ViewBag.location = values2?.location;
            ViewBag.videoUrl = values2?.videoUrl;

            DateTime date1=DateTime.Now;
            DateTime date2=values.AdvertisementDate;
            TimeSpan timeSpan= date1 - date2;
            int month = timeSpan.Days;
            ViewBag.datediff = month / 30;
            string slugFromTitle = CreateSlug(values.title);
            ViewBag.slugUrl = slugFromTitle;
            return View(values);
            
        }
        private string CreateSlug(string title) 
        {
            title = title.ToLowerInvariant();
            title = title.Replace(" ", "-");
            title = System.Text.RegularExpressions.Regex.Replace(title, @"[^a-z0-9\s-]"," ");
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+"," ").Trim();
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s","-").Trim();
            return title;
        }
    }
}
