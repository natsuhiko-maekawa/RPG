using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Paralysis : AbstractAilment
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Paralysis;
        }
    }
}