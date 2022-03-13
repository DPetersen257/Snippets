using System.Text;

namespace EpiqExtensions.Common;
public static class ObjectExtensions
{
    public static string PropertiesToString(this object obj)
    {
        var props = obj.GetType().GetProperties();
        var sb = new StringBuilder();
        foreach (var p in props)
            sb.Append(p.Name + "=" + p.GetValue(obj, null) + " ");

        return sb.ToString();
    }
}
