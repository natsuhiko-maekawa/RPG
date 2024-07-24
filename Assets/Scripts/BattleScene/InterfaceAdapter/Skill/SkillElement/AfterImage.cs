using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class AfterImage : AbstractBuff
    {
        public override BuffCode GetBuff()
        {
            return BuffCode.Speed;
        }

        public override float GetBuffRate()
        {
            return 2.0f;
        }

        public override int GetTurn()
        {
            return 5;
        }

        public override LifetimeCode GetLifetimeCode()
        {
            return LifetimeCode.ToEndTurn;
        }
    }
}