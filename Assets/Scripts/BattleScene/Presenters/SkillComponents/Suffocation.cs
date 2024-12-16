using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Suffocation : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Suffocation;
    }
}