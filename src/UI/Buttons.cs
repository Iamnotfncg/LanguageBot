using Telegram.Bot.Types.ReplyMarkups;
using LanguageBot.Language;

namespace LanguageBot.UI
{
    public static class Buttons
    {
        public static InlineKeyboardMarkup LanguageKeyboard()
        {
            var keyboardButtons = new List<InlineKeyboardButton[]>();

            // Create rows of buttons using the enum
            foreach (Languages language in Enum.GetValues(typeof(Languages)))
            {
                keyboardButtons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData(language.GetDescription(), $"lang_{language}")
                });
            }

            // Add a "Done" button
            keyboardButtons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData("Done", "done")
            });

            return new InlineKeyboardMarkup(keyboardButtons);
        }
    }
}