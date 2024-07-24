using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class MusterStrength : AbstractBuff
    {
        public override BuffCode GetBuff()
        {
            return BuffCode.Attack;
        }

        public override float GetBuffRate()
        {
            return 2.0f;
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