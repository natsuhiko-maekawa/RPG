using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class StarShell : AbstractBuff
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