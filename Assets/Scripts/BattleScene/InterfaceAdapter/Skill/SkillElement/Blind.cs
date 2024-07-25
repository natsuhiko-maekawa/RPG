using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Blind : AbstractAilment
    {
        public override AilmentCode AilmentCode { get; } = AilmentCode.Blind;
    }
}