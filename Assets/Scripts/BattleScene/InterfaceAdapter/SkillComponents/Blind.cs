using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class Blind : BaseAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Blind;
    }
}