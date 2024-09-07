using LanguageBot.Language;

namespace LanguageBot.Model.User
{
    public class User
    {
        public long UserId { get; set; }
        public Languages PreferredLanguages { get; set; }
    }
}