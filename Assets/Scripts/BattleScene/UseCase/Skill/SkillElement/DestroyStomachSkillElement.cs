using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class DestroyStomachSkillElement : DestroyPartSkillElement
    {
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Stomach;
        }
    }
}