using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class Wabisuke : AbstractBuff
    {
        public override BuffCode GetBuff()
        {
            return BuffCode.Speed;
        }

        public override float GetBuffRate()
        {
            return 0.5f;
        }

        public override int GetTurn()
        {
            return 10;
        }

        public override LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToEndTurn;
        }
    }
}