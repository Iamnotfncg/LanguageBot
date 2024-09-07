using Telegram.Bot.Types.ReplyMarkups;
using LanguageBot.Language;

namespace LanguageBot.UI
{
    public static class Buttons
    {
        public static InlineKeyboardMarkup LanguageKeyboard()
        {
            var keyboardButtons = new List<InlineKeyboardButton[]>();

            foreach (Languages language in Enum.GetValues(typeof(Languages)))
            {
                keyboardButtons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData(language.GetDescription(), $"lang_{language}")
                });
            }

            keyboardButtons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData("Done", "done")
            });

            return new InlineKeyboardMarkup(keyboardButtons);
        }
    }
}