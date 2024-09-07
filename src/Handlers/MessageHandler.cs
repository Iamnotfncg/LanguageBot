using Telegram.Bot;
using Telegram.Bot.Types;
using LanguageBot.UI;

namespace LanguageBot.Handlers
{
    public static class MessageHandler
    {
        public static async Task HandleMessageAsync(ITelegramBotClient botClient, Message message)
        {
            var chatId = message.Chat.Id;

            // Send language selection keyboard
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Please choose your language(s):",
                replyMarkup: Buttons.LanguageKeyboard()
            );
        }
    }
}