using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Paralysis : AbstractAilment
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Paralysis;
        }
    }
}