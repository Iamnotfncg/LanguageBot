using System;
using System.ComponentModel;

namespace LanguageBot.Language
{
    [Flags]
    public enum Languages
    {
        [Description("Spanish es")]    Spanish     = 1 << 1,
        [Description("English en")]    English     = 1 << 2,
        [Description("Ukrainian ua")]  Ukrainian   = 1 << 3,
        [Description("German de")]     German      = 1 << 4,
        None
    }

    public static class LanguageDispatcher
    {
        public static bool IsLanguageType(Languages language, Languages selectedByUser) => (language & selectedByUser) == language;
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}