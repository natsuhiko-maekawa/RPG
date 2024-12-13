using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Suffocation : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Suffocation;
    }
}