using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Wabisuke : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.Speed;
        public override float Rate { get; } = 0.5f;
        public override int Turn { get; } = 10;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;

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