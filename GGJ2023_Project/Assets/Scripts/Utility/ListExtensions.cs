using System.Collections.Generic;

public static class ListExtensions
{
    public static bool AddUnique<T>(this List<T> list, T item) {
        if (!list.Contains(item)) {
            list.Add(item);
            return true;
        }
        return false;
    }

    public static bool TryRemove<T>(this List<T> list, T item)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            return true;
        }
        return false;
    }
}