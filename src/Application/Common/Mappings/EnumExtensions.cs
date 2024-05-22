namespace HumanResourceManagement.Application.Common.Mappings;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
    {
        if (!Enum.TryParse(value, true, out TEnum enumValue))
        {
            throw new ArgumentException($"Invalid value '{value}' for enum type {typeof(TEnum).Name}");
        }

        return enumValue;
    }

    public static string ToEnumString<TEnum>(this TEnum value) where TEnum : struct
    {
        return Enum.GetName(typeof(TEnum), value);
    }
}
