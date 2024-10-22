using System.Reflection;

namespace Library.Domain.Enums.Extentions;

public static class EnumExtention
{
    public static string StringValue<T>(this T value)
       where T : Enum
    {
        var fieldName = value.ToString();
        var field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        return field?.GetCustomAttribute<StringValueAttribute>()?.Value ?? fieldName;
    }
}
