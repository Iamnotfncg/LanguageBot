using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using LanguageBot.Handlers;
using LanguageBot.Data;
using Telegram.Bot.Types.Enums;

namespace LanguageBot.Services
{
    public class BotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly BotDbContext _dbContext;

        public BotService(ITelegramBotClient botClient, BotDbContext dbContext)
        {
            _botClient = botClient;
            _dbContext = dbContext;
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
            if (update.Type == UpdateType.Message && update.Message != null)
                await MessageHandler.HandleMessageAsync(botClient, update.Message, _dbContext);
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
                await CallbackHandler.HandleCallbackAsync(botClient, update.CallbackQuery, _dbContext);
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }
    }
}