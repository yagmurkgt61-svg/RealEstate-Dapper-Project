using Microsoft.AspNetCore.Http;
using RealEstate_Dapper_UI.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RealEstate_Dapper_UI.Middlewares
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
                string stackTrace = ex.StackTrace.Length > 500
                        ? ex.StackTrace.Substring(0, 500)
                        : ex.StackTrace;

                string errorMessage = $"❌ *UI Tarafında Hata Yakalandı!* \n\n" +
                                     $"📍 **Sayfa:** {context.Request.Path}\n" +
                                     $"⚠️ **Hata Mesajı:** {ex.Message}\n" +
                                     $"<b>🔍 Yer:</b> {stackTrace}\n"+
                                    $"🕒 **Zaman:** {DateTime.Now:dd.MM.yyyy HH:mm}";

                TelegramHelper.SendMessage(TelegramHelper.TelegramChatIds.RealEstateError, errorMessage);
                throw;
            }
        }
    }
}
