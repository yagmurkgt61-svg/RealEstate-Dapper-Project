using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Categories");
            var responseMessage1 = await client.GetAsync("/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                if (responseMessage1.IsSuccessStatusCode)
                {
                    var jsonData1 = await responseMessage.Content.ReadAsStringAsync();
                    var values1 = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                    var model = new CompositeViewModel
                    {
                        CategoryList = values,
                        ProductList = values1
                    };
                    return View(model);
                }

             
            }


            return View(new CompositeViewModel { CategoryList = new List<ResultCategoryDto>() });
        }
        [HttpGet]
        public async Task<PartialViewResult> PartialSearch()
        {
            
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return PartialView(values);
            }
            return PartialView();

        }
        [HttpPost]
        public IActionResult PartialSearch(string searchKeyValue,int propertyCategoryId, string city) 
        {
            TempData["searchKeyValue"] = searchKeyValue;
            TempData["propertyCategoryId"] = propertyCategoryId;
            TempData["city"] = city;
            return RedirectToAction("PropertyListWithSearch","Property");
        }
    }
}
