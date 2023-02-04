public static class ArrayExtensions
{
	public static bool Contains<T>(this T[] array, T item)
	{
		foreach (var ele in array)
		{
			if (ele.Equals(item))
			{
				return true;
			}
		}
		return false;
	}
}