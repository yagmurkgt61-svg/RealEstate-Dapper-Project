using Microsoft.AspNetCore.SignalR;

namespace RealEstate_Dapper_Api.Hubs
{


    public class SignalRHub:Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SignalRHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task SendCategoryCount() 
        {
            // API'ye istek atarak kategori sayısını alır ve SignalR ile tüm kullanıılara gönderir.
            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:44319/api/Statistics/CategoryCount");
            var value1 = await responseMessage1.Content.ReadAsStringAsync();
            await Clients.All.SendAsync("ReceiveCategoryCount", value1);
        }
    }
}
