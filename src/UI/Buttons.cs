using LanguageBot.Language;
using Telegram.Bot.Types.ReplyMarkups;

namespace LanguageBot.UI
{
    public static class Buttons
    {
        public static InlineKeyboardMarkup MainMenuKeyboard()
        {
            var keyboardButtons = new List<InlineKeyboardButton[]>
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Choose Language", "choose_language")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("My Languages", "my_languages")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Delete My Account", "delete_account")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Start Learning", "start_learning")
                }
            };

            return new InlineKeyboardMarkup(keyboardButtons);
        }

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