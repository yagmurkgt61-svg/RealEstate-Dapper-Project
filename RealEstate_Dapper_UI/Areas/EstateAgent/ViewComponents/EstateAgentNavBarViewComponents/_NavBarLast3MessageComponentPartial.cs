using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.MessageDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Services;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.ViewComponents.EstateAgentNavBarViewComponents
{
    public class _NavBarLast3MessageComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        public _NavBarLast3MessageComponentPartial(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var id = _loginService.GetUserId;
            var client = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage = await client.GetAsync("/api/Messages?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultInBoxMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
