using System.Collections.Generic;

namespace BattleScene.UseCases
{
    public static class Constant
    {
        public const int AttackCountUpperLimit = 25;

        // GUI系
        public const string SelectActionInfo = "\u21c5で選択 / Spaceで決定";
        public const string SelectSkillInfo = "\u21c5で選択 / Spaceで決定 / Cで戻る";
        public const string SelectTargetInfo = "\u21c4で選択 / Spaceで決定 / Cで戻る";

        public static readonly IReadOnlyList<string> OptionList = new[] { "戦闘を続ける", "戦闘をやめる" };
    }
}