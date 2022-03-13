namespace EpiqExtensions.Common;
public static class ArrayExtensions
{
    public static string Delimit<T>(this T[] array, string delimiter)
    {
        if (array is string[])
            return string.Join(delimiter, array as string[]);
        var strings = new List<string>();
        foreach (T item in array)
        {
            if (item is not null)
#pragma warning disable CS8604 // Possible null reference argument.
                strings.Add(item.ToString());
        }
        return string.Join(delimiter, strings.ToArray());
    }

    public static bool In<T>(this T source, params T[] list)
    {
        ArgumentNullException.ThrowIfNull(source);
        return list.Contains(source);
    }
}
