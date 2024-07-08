using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.Interface;

namespace BattleScene.UseCases.Skill.SkillElement.AbstractClass
{
    public abstract class AbstractDestroyPart : ILuckSkillElement
    {
        public float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract BodyPartCode GetDestroyPart();
    }
}