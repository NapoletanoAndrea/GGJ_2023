using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) {
        if (dictionary.ContainsKey(key)) {
            dictionary.Remove(key);
        }
        
        dictionary.Add(key, value);
    }
}