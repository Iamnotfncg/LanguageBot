using LanguageBot.Language;
using LanguageBot.UI;
using Telegram.Bot.Types;
using Telegram.Bot;

public static class CallbackHandler
{
    private static bool isChoosingLanguage = false;
    public static int LastMessageId { get; private set; }

    public static async Task HandleCallbackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, BotDbContext dbContext)
    {
        var chatId = callbackQuery.Message.Chat.Id;

        if (callbackQuery.Data.StartsWith("lang_"))
        {
            var selectedLanguage = callbackQuery.Data.Substring(5);
            var languageEnum = Enum.Parse<Languages>(selectedLanguage);

            var user = await dbContext.Users.FindAsync(chatId);
            if (user != null)
            {
                if (user.UserProgress.SelectedLanguages.HasFlag(languageEnum))
                    user.UserProgress.SelectedLanguages &= ~languageEnum;
                else
                    user.UserProgress.SelectedLanguages |= languageEnum;

                await dbContext.SaveChangesAsync();

                var selectedLanguages = user.UserProgress.SelectedLanguages;
                var languagesList = selectedLanguages.ToString().Replace(", ", "\n");

                if (!isChoosingLanguage)
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"You have selected {languagesList}"
                    );

                    isChoosingLanguage = true;
                    LastMessageId = sentMessage.MessageId;
                }
                else
                {
                    await botClient.EditMessageTextAsync(
                        chatId: chatId,
                        messageId: LastMessageId,
                        text: $"You have selected {languagesList}"
                    );
                }
            }
        }
        else if (callbackQuery.Data == "done")
        {
            isChoosingLanguage = false;

            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Thank you! Your preferences have been saved."
            );
        }
        else
        {
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
                    var user = await dbContext.Users.FindAsync(chatId);
                    if (user != null)
                    {
                        var selectedLanguages = user.UserProgress.SelectedLanguages;
                        var languageCode = selectedLanguages.ToString().Split(',').FirstOrDefault()?.Trim();

                        if (!string.IsNullOrEmpty(languageCode))
                        {
                            var randomWord = await ApiWordService.GetRandomWordAsync(languageCode);
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: $"Here is a random word in {languageCode}: {randomWord}"
                            );
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "No language selected for learning."
                            );
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "User data not found."
                        );
                    }
                    break;

                default:
                    await botClient.AnswerCallbackQueryAsync(
                        callbackQueryId: callbackQuery.Id,
                        text: "Unknown command."
                    );
                    break;
            }
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
            await botClient.SendTextMessageAsync(chatId: chatId, text: "User data not found.");
        }
    }

    public static async Task SendSelectedLanguagesAsync(ITelegramBotClient botClient, long chatId, BotDbContext dbContext)
    {
        var user = await dbContext.Users.FindAsync(chatId);

        if (user != null)
        {
            var selectedLanguages = user.UserProgress.SelectedLanguages;

            if (selectedLanguages == Languages.None)
            {
                await botClient.SendTextMessageAsync(chatId, "You have not selected any languages.");
            }
            else
            {
                var languagesList = selectedLanguages.ToString().Replace(", ", "\n");

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"You have selected the following language(s):\n{languagesList}"
                );
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(chatId, "User data not found. Please start by choosing your languages.");
        }
    }
}