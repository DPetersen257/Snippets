using System.Data;
using System.Reflection;

namespace EpiqExtensions.Common;
public static class ListExtensions
{
    /// <summary>
    /// Converts a List<T> to a data table.
    /// </summary>
    /// <typeparam name="T">The type of the list</typeparam>
    /// <param name="items">The list to convert.</param>
    /// <returns>A datatable</returns>
    public static DataTable ConvertToDataTable<T>(this List<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        var dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table
            var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;

            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }

        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }

            dataTable.Rows.Add(values);
        }

        //put a breakpoint here and check datatable
        return dataTable;
    }

    public static List<T>[] Partition<T>(this List<T> list, int partSize)
    {
        ArgumentNullException.ThrowIfNull(list);

        int count = (int)Math.Ceiling(list.Count / (double)partSize);
        var partitions = new List<T>[count];
        int k = 0;
        for (int i = 0; i < partitions.Length; i++)
        {
            partitions[i] = new List<T>(partSize);
            for (int j = k; j < k + partSize; j++)
            {
                if (j >= list.Count)
                    break;
                partitions[i].Add(list[j]);
            }
            k += partSize;
        }
        return partitions;
    }

    public static List<T>[] Divide<T>(this List<T> list, int totalPartitions)
    {
        if (totalPartitions < 1)
            throw new ArgumentOutOfRangeException(nameof(totalPartitions));

        var partitions = new List<T>[totalPartitions];
        int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
        int k = 0;

        for (int i = 0; i < partitions.Length; i++)
        {
            partitions[i] = new List<T>();
            for (int j = k; j < k + maxSize; j++)
            {
                if (j >= list.Count)
                    break;
                partitions[i].Add(list[j]);
            }
            k += maxSize;
        }
        return partitions;
    }
}
