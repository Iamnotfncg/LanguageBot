using Telegram.Bot.Types.ReplyMarkups;

public static class Buttons
{
    // var LanguageButtons = ...

    public static InlineKeyboardMarkup LanguageKeyboard()
    {
        var keyboardButtons = new List<InlineKeyboardButton[]>();

        // Create rows of buttons
        foreach (var language in LanguageButtons)
        {
            keyboardButtons.Add(new[]
            {
                    InlineKeyboardButton.WithCallbackData(language.Key, language.Value)
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