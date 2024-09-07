using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using LanguageBot.Handlers;

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
    }
}