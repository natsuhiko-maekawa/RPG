using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class DestroyLegSkillElement : DestroyPartSkillElement
    {
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Leg;
        }
    }
}