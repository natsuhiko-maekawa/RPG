using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

namespace BattleScene.InterfaceAdapter.PrimeSkill
{
    public class Utsusemi : BaseBuff
    {
        public override BuffCode BuffCode { get; } = BuffCode.UtsusemiSkill;
        public override int Turn { get; } = 1;
        public override LifetimeCode LifetimeCode { get; } = LifetimeCode.ToEndTurn;

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