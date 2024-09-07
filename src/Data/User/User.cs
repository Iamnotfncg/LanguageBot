using LanguageBot.Language;
using Microsoft.EntityFrameworkCore;

namespace LanguageBot.Data
{
    public class User
    {
        public long UserId { get; set; }
        public Progress UserProgress { get; set; }

        public User()
        {
            UserProgress = new Progress();
        }

        public User(long userId) : this()
        {
            UserId = userId;
        }
    }

    [Owned]
    public class Progress
    {
        public Languages SelectedLanguages { get; set; }
        public void AddLanguage(Languages language) { SelectedLanguages |= language; }
        public void RemoveLanguage(Languages language) { SelectedLanguages &= ~language; }
        public bool HasLanguage(Languages language) => (SelectedLanguages & language) == language;
    }
}