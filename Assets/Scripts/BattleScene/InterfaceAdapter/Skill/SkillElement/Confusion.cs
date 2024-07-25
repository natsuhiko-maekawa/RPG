using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Confusion : AbstractAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Confusion;

        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Confusion;
        }
    }
}