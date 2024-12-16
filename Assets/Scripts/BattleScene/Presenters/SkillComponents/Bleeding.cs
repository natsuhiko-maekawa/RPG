using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents.BaseClass;

namespace BattleScene.Presenters.SkillComponents
{
    public class Bleeding : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Bleeding;
    }
}