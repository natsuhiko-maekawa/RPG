using BattleScene.Domain.Code;

namespace BattleScene.UseCase.Skill.SkillElement.AbstractClass
{
    public abstract class BuffSkillElement : BaseClass.SkillElement
    {
        public abstract BuffCode GetBuff();
        public abstract float GetBuffRate();
        public abstract int GetTurn();
        public abstract LifetimeCode GetLifetimeCode();
    }
}