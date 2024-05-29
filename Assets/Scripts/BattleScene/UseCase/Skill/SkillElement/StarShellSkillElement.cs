using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class StarShellSkillElement : BuffSkillElement
    {
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