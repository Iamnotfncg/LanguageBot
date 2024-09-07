using LanguageBot.Services;
using Telegram.Bot;

class Program
{
    static async Task Main()
    {
        var botClient = new TelegramBotClient("YOUR_BOT_TOKEN");
        var botService = new BotService(botClient);

        await botService.StartAsync();

        Console.WriteLine("Bot is running. Press any key to exit.");
        Console.ReadKey();
    }
}