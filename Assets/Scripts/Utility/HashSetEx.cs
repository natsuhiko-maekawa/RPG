using System.Collections.Generic;

namespace Utility
{
    public static class HashSetEx
    {
        public static void Update<T>(this HashSet<T> hashSet, T item)
        {
            if (hashSet.TryGetValue(item, out var result))
                hashSet.Remove(result);

            hashSet.Add(item);
        }
    }
}