using RealEstate_Dapper_Api.Services;

namespace RealEstate_Dapper_Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TelegramService telegramService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string errorMessage = $"<b>❌ API Tarafında Hata Yakalandı!</b>\n\n" +
                                     $"<b>📍 Endpoint:</b> {context.Request.Path}\n" +
                                     $"<b>🛠 Metot:</b> {context.Request.Method}\n" +
                                     $"<b>⚠️ Mesaj:</b> {ex.Message}\n" +
                                     $"<b>🕒 Zaman:</b> {DateTime.Now:dd.MM.yyyy HH:mm}";
                  TelegramHelper.SendMessage(TelegramHelper.TelegramChatIds.RealEstateError, errorMessage);

                throw;
            }
        }
    }
}
