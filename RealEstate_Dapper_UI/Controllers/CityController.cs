using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CityDtos;


namespace RealEstate_Dapper_UI.Controllers
{
    public class CityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task< IActionResult> Index()
        {

            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/City/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCityDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
