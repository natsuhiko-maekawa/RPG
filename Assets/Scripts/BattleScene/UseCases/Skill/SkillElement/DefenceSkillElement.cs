using System;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class DefenceSkillElement : BuffSkillElement
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