using System;
using System.ComponentModel;

namespace LanguageBot.Language
{

    [Flags]
    public enum Languages
    {
        [Description("Spanish")]    Spanish     = 1 << 0,
        [Description("English")]    English     = 1 << 1,
        [Description("Ukrainian")]  Ukrainian   = 1 << 2,
        [Description("German")]     German      = 1 << 3,
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