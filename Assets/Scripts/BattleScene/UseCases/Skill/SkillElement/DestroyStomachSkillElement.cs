using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class DestroyStomachSkillElement : DestroyPartSkillElement
    {
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Stomach;
        }
    }
}