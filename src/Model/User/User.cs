using LanguageBot.Language;

namespace LanguageBot.Model.User
{
    public class User
    {
        public long UserId { get; set; }
        public Progress UserProgress { get; set; }

        public User(long userId)
        {
            UserId = userId;
            UserProgress = new Progress();
        }

        public class Progress
        {
            public Languages SelectedLanguages { get; set; }

            public void AddLanguage(Languages language) { SelectedLanguages |= language; }
            public void RemoveLanguage(Languages language) { SelectedLanguages &= ~language; }
            public bool HasLanguage(Languages language) => (SelectedLanguages & language) == language;
        }
    }
}