using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class UtsusemiSkillElement : BuffSkillElement
    {
        public override BuffCode GetBuff()
        {
            return BuffCode.UtsusemiSkill;
        }

        public override float GetBuffRate()
        {
            return default;
        }

        public override int GetTurn()
        {
            return 1;
        }

        public override LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToEndTurn;
        }
    }
}