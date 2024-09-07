using Telegram.Bot;
using Telegram.Bot.Types;
using LanguageBot.Data;
using LanguageBot.Language;
using LanguageBot.UI;
using LanguageBot.Handlers;

namespace LanguageBot.Handlers
{
    public static class CallbackHandler
    {
        public static async Task HandleCallbackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, BotDbContext dbContext)
        {
            var chatId = callbackQuery.Message.Chat.Id;

            if (callbackQuery.Data.StartsWith("lang_"))
            {
                var selectedLanguage = callbackQuery.Data.Substring(5);

                // Update user progress in the database if needed
                var user = await dbContext.Users.FindAsync(chatId);
                if (user != null)
                {
                    // Assume there's a method to update user's language selection
                    user.UserProgress.SelectedLanguages |= Enum.Parse<Languages>(selectedLanguage);
                    await dbContext.SaveChangesAsync();
                }

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

            switch (callbackQuery.Data)
            {
                case "choose_language":
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Please choose your language(s):",
                        replyMarkup: Buttons.LanguageKeyboard()
                    );
                    break;

                case "my_languages":
                    await SendSelectedLanguagesAsync(botClient, chatId, dbContext);
                    break;

                case "delete_account":
                    await DeleteAccountAsync(botClient, chatId, dbContext);
                    break;

                case "start_learning":
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Starting learning process."
                    );
                    break;

                default:
                    await botClient.AnswerCallbackQueryAsync(
                        callbackQueryId: callbackQuery.Id,
                        text: "Unknown command."
                    );
                    break;
            }
        }
        public static async Task DeleteAccountAsync(ITelegramBotClient botClient, long chatId, BotDbContext dbContext)
        {
            var user = await dbContext.Users.FindAsync(chatId);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Your account has been deleted."
                );
            }
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "User data not found."
                );
            }
        }

        public static async Task SendSelectedLanguagesAsync(ITelegramBotClient botClient, long chatId, BotDbContext dbContext)
        {
            var user = await dbContext.Users.FindAsync(chatId);

            if (user != null)
            {
                var selectedLanguages = user.UserProgress.SelectedLanguages;

                if (selectedLanguages == Languages.None)
                    await botClient.SendTextMessageAsync(chatId, "You have not selected any languages.");
                else
                {
                    var languagesList = selectedLanguages
                        .ToString()
                        .Replace(", ", "\n");

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"You have selected the following language(s):\n{languagesList}"
                    );
                }
            }
            else
                await botClient.SendTextMessageAsync(chatId, "User data not found. Please start by choosing your languages.");
        }
    }
}