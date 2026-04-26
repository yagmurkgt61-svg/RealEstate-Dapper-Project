namespace RealEstate_Dapper_UI.Services
{
    public class TelegramService
    {
        private readonly HttpClient _httpClient;
        private readonly string _botToken = "8594758207:AAH3tPGbwXKI3McHOFdkHP0jdv8Abo2tqR0";
        private readonly string _chatId = "7130389211"; 

        public TelegramService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendErrorMessage(string message)
        {
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={Uri.EscapeDataString(message)}&parse_mode=Markdown";
            await _httpClient.GetAsync(url);
        }
    }
}

