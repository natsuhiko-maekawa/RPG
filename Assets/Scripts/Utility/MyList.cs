using System.Collections.Generic;

namespace Utility
{
    public static class MyList<T>
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private static readonly List<T> List = new();
        public static IReadOnlyList<T> Empty() => List;
    }
}