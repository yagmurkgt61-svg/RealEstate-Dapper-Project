using System.ComponentModel;
using Telegram.Bot;
using System.Linq;
namespace RealEstate_Dapper_Api.Services
{
    public static class TelegramHelper
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("8214736707:AAH4Fh7tHayP7dY5EV-QuBgKDt-i7FKANVw");

        public enum TelegramChatIds
        {
            [Description("-5188160290")]
            RealEstateError

        }

        public static async Task SendMessage(TelegramChatIds chatId, string message)
        {
            try
            {
                var send = Bot.SendMessage(chatId: new Telegram.Bot.Types.ChatId(chatId.GetDescriptionFromEnumValue()), text: message);
                var res = send.Result;
            }
            catch (Exception e)
            {
            }
        }


        public static void SendMessage(this Exception ex, TelegramChatIds chatId, string message)
        {
            try
            {
                var err = ex.ToString().Length > 1900 ? ex.ToString()[..1900] : ex.ToString();
                message = message + " Hata Detayı: " + err;
                var send = Bot.SendMessage(chatId: new Telegram.Bot.Types.ChatId(chatId.GetDescriptionFromEnumValue()), text: message);
                var res = send.Result;
            }
            catch (Exception e)
            {
            }
        }

        //public static void SendFile(TelegramChatIds chatId, string fileTitle, string file)
        //{
        //    //(!File.Exists(file)) return;

        //    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        //    {
        //        var inputFile = new Telegram.Bot.Types.InputFileStream(fs, fileTitle);
        //        var send = Bot.SendDocumentAsync(
        //            chatId: new Telegram.Bot.Types.ChatId(chatId.GetDescriptionFromEnumValue()),
        //            document: inputFile,
        //            caption: fileTitle
        //        );

        //        var res = send.Result;
        //    }
        //}


        private static string GetDescriptionFromEnumValue(this TelegramChatIds value)
        {
            return !(value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() is DescriptionAttribute attribute)
                ? value.ToString()
                : attribute.Description;
        }
    }
}
