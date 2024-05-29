using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class AbsoluteConfusionSkillElement : AilmentSkillElement
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