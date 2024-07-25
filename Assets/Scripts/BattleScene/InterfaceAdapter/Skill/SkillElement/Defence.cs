using System;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Defence : AbstractBuff
    {
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