using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Paralysis : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Paralysis;
    }
}