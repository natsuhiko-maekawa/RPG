using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Burning : BaseSlip
    {
        public override SlipCode SlipCode { get; } = SlipCode.Burning;
    }
}