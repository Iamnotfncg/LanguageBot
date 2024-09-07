namespace LanguageBot.Language
{
    [Flags]
    internal enum Languages
    {
        Spanish = 1 << 0,
        English = 1 << 1,
        Ukrainian = 1 << 2,
        German = 1 << 3,
    }
    internal static class LanguageDispatcher
    {
        internal static bool IsLanguageType(Languages language, Languages selectedByUser) => (language & selectedByUser) == language;
    }
}