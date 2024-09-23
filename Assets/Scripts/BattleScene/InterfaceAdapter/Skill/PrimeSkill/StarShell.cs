using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class StarShell : AbstractBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.HitRate;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 15;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;

        public override BuffCode GetBuff()
        {
            return BuffCode.HitRate;
        }

        public override float GetBuffRate()
        {
            return 2.0f;
        }

        public override int GetTurn()
        {
            return 15;
        }

        public override LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToEndTurn;
        }
    }
}