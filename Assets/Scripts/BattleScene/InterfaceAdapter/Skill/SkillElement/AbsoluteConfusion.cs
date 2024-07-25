using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class AbsoluteConfusion : AbstractAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Confusion;
        public override float LuckRate { get; } = 1.0f;


    }
}