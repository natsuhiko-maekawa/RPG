namespace Utility
{
    public static class BitUtility
    {
        /// <summary>
        /// ビットをカウントして返すメソッド。<br/>
        /// <see href="https://github.com/dotnet/corert/blob/c6af4cfc8b625851b91823d9be746c4f7abdc667/src/System.Private.CoreLib/shared/System/Numerics/BitOperations.cs#L207">BitOperations.cs</see>
        /// を参考に実装。
        /// </summary>
        /// <param name="value">符号なし整数。</param>
        /// <returns>カウントしたビットの合計。</returns>
        public static int BitCount(uint value)
        {
            const uint c1 = 0x_55555555u;
            const uint c2 = 0x_33333333u;
            const uint c3 = 0x_0F0F0F0Fu;
            const uint c4 = 0x_01010101u;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 24;

            return (int)value;
        }
    }
}