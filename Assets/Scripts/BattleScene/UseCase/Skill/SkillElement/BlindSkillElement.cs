using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class BlindSkillElement : AilmentSkillElement
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Blind;
        }
    }
}