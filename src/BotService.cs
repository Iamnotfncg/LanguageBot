using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using LanguageBot.Handlers;
using LanguageBot.Language;
using LanguageBot.Model.User;

namespace LanguageBot.Services
{
    public class BotService
    {
        private readonly ITelegramBotClient _botClient;

        public BotService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task StartAsync()
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions
            );
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Handle updates (e.g., messages, callbacks)
            if (update.Type == UpdateType.Message)
            {
                await MessageHandler.HandleMessageAsync(botClient, update.Message);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await CallbackHandler.HandleCallbackAsync(botClient, update.CallbackQuery);
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }

        private static async Task SendSelectedLanguagesAsync(ITelegramBotClient botClient, long chatId)
        {
            if (Users.TryGetValue(chatId, out var user))
            {
                var selectedLanguages = user.UserProgress.SelectedLanguages;

                if (selectedLanguages == Languages.None)
                {
                    await botClient.SendTextMessageAsync(chatId, "You have not selected any languages.");
                }
                else
                {
                    var languagesList = selectedLanguages
                        .ToString()
                        .Replace(", ", "\n"); // Formatting each language on a new line

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
}