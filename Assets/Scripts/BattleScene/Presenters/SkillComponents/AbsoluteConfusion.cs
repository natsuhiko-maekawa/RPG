using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class AbsoluteConfusion : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Confusion;
        public override float LuckRate { get; } = 1.0f;
    }
}