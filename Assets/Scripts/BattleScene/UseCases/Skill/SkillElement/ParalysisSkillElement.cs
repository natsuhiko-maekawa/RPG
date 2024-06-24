using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class ParalysisSkillElement : AilmentSkillElement
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Paralysis;
        }
    }
}