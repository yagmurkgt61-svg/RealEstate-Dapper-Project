using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Models.Repositories.BottomGridRepositories;
using RealEstate_Dapper_Api.Services;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottomGridsController : ControllerBase
    {
        private readonly IBottomGridRepository _bottomGridRepository;

        public BottomGridsController(IBottomGridRepository bottomGridRepository)
        {
            _bottomGridRepository = bottomGridRepository;
        }
        //[HttpGet("telegram-chat-ids")]
        //public async Task<IActionResult> GetTelegramChatIds()
        //{
        //    var token = "8214736707:AAH4Fh7tHayP7dY5EV-QuBgKDt-i7FKANVw"; // < > koyma
        //    var url = $"https://api.telegram.org/bot{token}/getUpdates";

        //    using var client = new HttpClient();
        //    var response = await client.GetAsync(url);

        //    if (!response.IsSuccessStatusCode)
        //        return BadRequest("Telegram API çağrısı başarısız");

        //    var content = await response.Content.ReadAsStringAsync();
        //    TelegramHelper.SendMessage(TelegramHelper.TelegramChatIds.RealEstateError, content);
        //    return Ok(content); // raw json döner
        //}
        [HttpGet]
        public async Task<IActionResult> BottomGridList()
        {
            var values = await _bottomGridRepository.GetAllBottomGridAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
        {
            _bottomGridRepository.CreateBottomGrid(createBottomGridDto);
            return Ok("Veri Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBottomGrid(int id)
        {
            _bottomGridRepository.DeleteBottomGrid(id);
            return Ok("Veri Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
        {
            _bottomGridRepository.UpdateBottomGrid(updateBottomGridDto);
            return Ok("Veri Başarılı Bir Şekilde Güncellendi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBottomGrid(int id)
        {
            var values = await _bottomGridRepository.GetBottomGrid(id);
            return Ok(values);

        }
    }
}
