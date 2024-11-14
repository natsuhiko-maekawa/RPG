using System.Collections.Generic;

namespace BattleScene.UseCases
{
    public static class Constant
    {
        public const int AttackCountUpperLimit = 25;
        public static readonly IReadOnlyList<string> OptionList = new[] { "戦闘を続ける", "戦闘をやめる" };
    }
}