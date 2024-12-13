namespace Common
{
    // constのバージョニング問題 (以下のページを参照) を避けるために、定数をget-only プロパティで定義している。
    // https://ufcpp.net/study/csharp/sp_const.html#versioning
    public static class Constant
    {
        public static int MaxEnemyCount { get; } = 4;
        public static int MaxOrderNumber { get; } = 14;
    }
}