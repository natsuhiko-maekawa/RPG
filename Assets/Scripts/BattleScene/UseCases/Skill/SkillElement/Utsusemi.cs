using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Utsusemi : AbstractBuff
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