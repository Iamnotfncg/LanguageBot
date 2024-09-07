using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using LanguageBot.Data;
using LanguageBot.Services;

class Program
{
    static async Task Main()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BotDbContext>()
            .UseNpgsql("bebebebebebebebebebebebebebe")
            .Options;

        using var dbContext = new BotDbContext(dbContextOptions);

        var botClient = new TelegramBotClient("bububububuububububu);
        var botService = new BotService(botClient, dbContext);

        await botService.StartAsync();

        Console.WriteLine("Bot is running. Press any key to exit.");
        Console.ReadKey();
    }
}