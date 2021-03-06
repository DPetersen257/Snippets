
namespace EpiqExtensions.Common;

/// <summary>
/// Source https://gist.github.com/cgbeutler/699e96f4868a7ccdd004d1daf38407d9
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EnumExtensions<T>
    where T : struct, Enum
  {
    /// <summary> An list of the enum values ordered as defined. Can contain duplicates. </summary>
    public static readonly IReadOnlyList<T> Values = Enum.GetValues( typeof( T ) ).Cast<T>().ToList();
    /// <summary> An ascending list of the enum values ordered by value. Duplicates removed. </summary>
    public static readonly IReadOnlyList<T> ValuesAscending = Enum.GetValues( typeof( T ) ).Cast<T>().Distinct().OrderBy( v => v ).ToList();
    /// <summary> An descending list of the enum values ordered by value. Duplicates removed. </summary>
    public static readonly IReadOnlyList<T> ValuesDescending = Enum.GetValues( typeof( T ) ).Cast<T>().Distinct().OrderByDescending( v => v ).ToList();

    /// <summary> An list of the enum value names ordered as defined. </summary>
    public static readonly IReadOnlyList<string> Names = Enum.GetNames( typeof( T ) ).ToList();
    /// <summary> An ascending list of the enum value names ordered alphabetically. </summary>
    public static readonly IReadOnlyList<string> NamesAscending = Enum.GetNames( typeof( T ) ).OrderBy( s => s ).ToList();
    /// <summary> An descending list of the enum value names ordered alphabetically. </summary>
    public static readonly IReadOnlyList<string> NamesDescending = Enum.GetNames( typeof( T ) ).OrderByDescending( s => s ).ToList();
    }