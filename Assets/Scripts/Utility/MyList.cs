using System.Collections.Generic;

namespace Utility
{
    public static class MyList<T>
    {
        /// <summary>
        /// 事前に割り当てられた空のリストの静的インスタンスを返す。ガベージコレクションの発生を抑えるために使用する。詳しくは
        /// <see href="https://docs.unity3d.com/ja/2022.1/Manual/performance-garbage-collection-best-practices.html#emptyarray">空配列の再利用</see>
        /// を参照のこと。
        /// </summary>
        public static IReadOnlyList<T> Empty => new List<T>();
    }
}