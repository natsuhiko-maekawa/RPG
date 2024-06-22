using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Skill.SkillElement.AbstractClass
{
    public abstract class DestroyPartSkillElement : BaseClass.SkillElement, ILuckSkillElement
    {
        public float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract BodyPartCode GetDestroyPart();
    }
}