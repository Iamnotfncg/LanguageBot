using Telegram.Bot;
using Telegram.Bot.Types;
using LanguageBot.UI;
using LanguageBot.Language;
using LanguageBot.Model.User;

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

        public static async Task HandleMessageAsync(ITelegramBotClient botClient, Message message)
        {
            var chatId = message.Chat.Id;
            var text = message.Text;

            if (text == "/start")
            {
                // Initialize user if not already present
                if (!Users.ContainsKey(chatId))
                {
                    Users[chatId] = new User(chatId);
                }

                // Send language selection keyboard
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Please choose your language(s):",
                    replyMarkup: Buttons.LanguageKeyboard()
                );
            }
            else if (text == "/mylanguages")
            {
                await SendSelectedLanguagesAsync(botClient, chatId);
            }
        }

        
    }
}