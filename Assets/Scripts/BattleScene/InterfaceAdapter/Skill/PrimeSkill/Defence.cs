using System;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class Defence : AbstractBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.DefenceSkill;
        public override float Rate { get; } = 2.0f;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToNextAction;

        public override BuffCode GetBuff()
        {
            return BuffCode.Defence;
        }

        public override float GetBuffRate()
        {
            throw new InvalidOperationException();
        }

        public override int GetTurn()
        {
            return 1;
        }

        public override LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToNextAction;
        }
    }
}