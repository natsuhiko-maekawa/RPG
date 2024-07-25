using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractBuff
    {
        public abstract BuffCode GetBuff();
        public abstract float GetBuffRate();
        public abstract int GetTurn();
        public abstract LifetimeCode GetLifetimeCode();
    }
}