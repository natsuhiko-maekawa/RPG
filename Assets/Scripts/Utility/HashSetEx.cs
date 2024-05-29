using System.Collections.Generic;

namespace Utility
{
    public static class HashSetEx
    {
        public static HashSet<T> Update<T>(this HashSet<T> hashSet, T item)
        {
            var newHashSet = new HashSet<T>(hashSet);
            if (newHashSet.TryGetValue(item, out var result))
                newHashSet.Remove(result);
            
            newHashSet.Add(item);
            return newHashSet;
        }
    }
}