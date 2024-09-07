using Telegram.Bot;
using Telegram.Bot.Types;
using LanguageBot.Language;

namespace LanguageBot.Handlers
{
    public static class CallbackHandler
    {
        public static async Task HandleCallbackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var chatId = callbackQuery.Message.Chat.Id;

            if (callbackQuery.Data.StartsWith("lang_"))
            {
                var selectedLanguage = callbackQuery.Data.Substring(5);
                await botClient.AnswerCallbackQueryAsync(
                    callbackQueryId: callbackQuery.Id,
                    text: $"You selected {selectedLanguage}."
                );
            }
            else if (callbackQuery.Data == "done")
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Thank you! Your preferences have been saved."
                );
            }
        }
    }
}