using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class AbsoluteConfusion : AbstractAilment
    {
        public override float GetLuckRate()
        {
            return 1.0f;
        }

        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Confusion;
        }
    }
}