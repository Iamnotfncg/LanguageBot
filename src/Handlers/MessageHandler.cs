using Telegram.Bot;
using Telegram.Bot.Types;
using LanguageBot.UI;
using LanguageBot.Language;
using LanguageBot.Data;

namespace LanguageBot.Handlers
{
    public static class MessageHandler
    {
        public static async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, BotDbContext dbContext)
        {
            var chatId = message.Chat.Id;
            var text = message.Text;

            if (text == "/start")
            {
                var user = await dbContext.Users.FindAsync(chatId);
                if (user == null)
                {
                    user = new LanguageBot.Data.User
                    {
                        UserId = chatId,
                        UserProgress = new Progress()
                    };
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                }

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Welcome! Choose an option:",
                    replyMarkup: Buttons.MainMenuKeyboard()
                );
            }
        }
    }
}