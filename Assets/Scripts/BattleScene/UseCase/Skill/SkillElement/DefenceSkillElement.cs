using System;
using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class DefenceSkillElement : IBuffSkill
    {
        public BuffCode GetBuff()
        {
            return BuffCode.Defence;
        }

        public float GetBuffRate()
        {
            throw new InvalidOperationException();
        }

        public int GetTurn()
        {
            return 1;
        }

        public LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToNextAction;
        }
    }
}