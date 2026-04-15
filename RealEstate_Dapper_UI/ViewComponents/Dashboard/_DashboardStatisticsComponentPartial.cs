using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.ViewComponents.Dashboard
{
    public class _DashboardStatisticsComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Statistics1 - ToplamİlanSayısı
            var client1 = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage1 = await client1.GetAsync("/api/Statistics/ProductCount");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            ViewBag.productCount = jsonData1;
            #endregion

            #region Statistics2 - En Başarılı Personel
            var client2 = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage2 = await client2.GetAsync("/api/Statistics/EmployeeNameByMaxProductCount");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.employeeNameByMaxProductCount = jsonData2;
            #endregion

            #region Statistics3 - İlandaki Şehir Sayıları
            var client3 = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage3 = await client3.GetAsync("/api/Statistics/DifferentCityCount");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.differentCityCount = jsonData3;
            #endregion

            #region Statistics4 - Ortalama KiraFiyatı
            var client4 = _httpClientFactory.CreateClient("RealEstateClient");
            var responseMessage4 = await client4.GetAsync("/api/Statistics/AverageProductPriceByRent");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.averageProductPriceByRent = jsonData4;
            #endregion
            return View();
        }
    }
}
